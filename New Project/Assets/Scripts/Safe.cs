using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Safe : Interactable
{
    [SerializeField] private CanvasGroup puzzleScreen;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject go;

    private Outline outline;
    private float rotationAmount = 0.5f;
    private float delaySpeed = 0.05f;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void Update() 
    {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
            puzzleScreen.alpha = 0f;
            puzzleScreen.gameObject.SetActive(false);
            FPSController.CanMove = true;

        }


    }
    public override void OnFocus()
    {
        outline.enabled = true;
    }

    public override void OnInteract()
    {
        Debug.Log("Safe clicked!");
        puzzleScreen.GetComponent<Canvas>().gameObject.SetActive(true); 
        puzzleScreen.alpha = 0.85f;
        FPSController.CanMove = false;
    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
        puzzleScreen.alpha = 0f;
    }

    public void openSafe()
    {
        FPSController.CanMove = true;
        puzzleScreen.gameObject.SetActive(false);
        go.gameObject.transform.Rotate(new Vector3(0, rotationAmount, 0));
        float count = 0;
        while (count <= 90)
        {
            go.gameObject.transform.Rotate(new Vector3(0, rotationAmount, 0));
            count += rotationAmount;
        }
        puzzleScreen.GetComponent<Canvas>().gameObject.SetActive(false);

    }

}

   

