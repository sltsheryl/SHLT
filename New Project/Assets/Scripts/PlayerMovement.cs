using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.8f;
    private Vector3 velocity;
    private float charHeight;
    private Transform thisTransform;
    public GameObject go;
    float distanceTraveled = 0f;

    private void Start()
    {
        charHeight = controller.height;
        thisTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        float h = charHeight;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y = gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey("c"))
        {
            h = charHeight / 10;
        }

        float lastHeight = controller.height;
        controller.height = Mathf.Lerp(controller.height, h, 10 * Time.deltaTime); ;
        Vector3 p = transform.position;
        p.y += (controller.height - lastHeight) / 4;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform)
                {
                    //print(hit.transform.gameObject.name);
                    string target = hit.transform.gameObject.name;
                    if (target == "wallA" || target == "wallB" || target == "wallC")
                    {
                        Debug.Log("Play");
                        if (distanceTraveled < 100000f)
                        {
                            Vector3 oldPosition = go.transform.position;
                            go.transform.Translate(0, 0, 10 * Time.deltaTime);
                            distanceTraveled += Vector3.Distance(oldPosition, go.transform.position);
                        }
                    }

                }
            }
        }
    }
}

