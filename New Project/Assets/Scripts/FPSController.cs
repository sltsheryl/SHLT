using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public static bool CanMove { get; set; } = true;
    public static bool CanLook { get; set; } = true;
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);
    private bool shouldJump => Input.GetKey(jumpKey) && characterController.isGrounded;
    private bool shouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && characterController.isGrounded;

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canInteract = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;


    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f; 
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f; 
    [SerializeField, Range(1, 100)] private float upperLookLimit = 80.0f; 
    [SerializeField, Range(1, 100)] private float lowerLookLimit = 80.0f;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 1.7f;
    [SerializeField] private float standingHeight = 3.4f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.85f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 1.7f, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;

    [Header("Interaction")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private Interactable currentInteractable;

    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            if (CanLook)
            {
                HandleMouseLook();
            } 

            if (canJump)
            {
                HandleJump();
            }

            if (canCrouch)
            {
                HandleCrouch();
            }

            if (canInteract)
            {
                HandleInteractionCheck();
                HandleInteractionInput();
            }
            ApplyFinalMovements();
        }
    }

    public static void EnableControls()
    {
        CanMove = true;
        CanLook = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void DisableControls()
    {
        CanMove = false;
        CanLook = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    private void HandleMovementInput()
    {
        float movementSpeed = isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : walkSpeed;
        currentInput = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        if (currentInput.magnitude > 1)
        {
            currentInput = currentInput.normalized;
        }
        currentInput *= movementSpeed;

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -lowerLookLimit, upperLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void HandleJump()
    {
        if (shouldJump)
        {
            moveDirection.y = jumpForce;
        }
    }

    private void HandleCrouch()
    {
        if (shouldCrouch)
        {
            StartCoroutine(CrouchStand());
        }
    }

    private void HandleInteractionCheck()
    {
        if (Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), 
            out RaycastHit hit,
            interactionDistance))
        {
            if (hit.collider.gameObject.layer == 9
                && (currentInteractable == null 
                    || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID()))
            {
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                {
                    currentInteractable.OnFocus();
                }
            }
        }
        else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }


    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactKey)
            && currentInteractable != null
            && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint),
                               out RaycastHit hit,
                               interactionDistance,
                               interactionLayer))
        {
            currentInteractable.OnInteract();
        }
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);

        if (characterController.velocity.y < 1 && characterController.isGrounded)
        {
            moveDirection.y = 0;
        }
    }

    private IEnumerator CrouchStand()
    {
        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;
        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }
}
