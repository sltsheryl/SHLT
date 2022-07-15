using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Safe : Interactable
{
    [SerializeField] private CanvasGroup puzzleScreen;
    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private GameObject go;

    private Outline outline;
    private float rotationAmount = 0.5f;
    private float delaySpeed = 0.05f;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;

    }

    public override void OnFocus()
    {
        outline.enabled = true;
    }

    public override void OnInteract()
    {
        Debug.Log("Safe clicked!");
        canvasManager.AddToCanvasStack(puzzleScreen);
    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
    }

    public void openSafe()
    {
        canvasManager.PopFromCanvasStack();
       
        go.gameObject.transform.Rotate(new Vector3(0, rotationAmount, 0));
        float count = 0;
        while (count <= 90)
        {
            go.gameObject.transform.Rotate(new Vector3(0, rotationAmount, 0));
            count += rotationAmount;
        }
     


    }

}

   

