using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Interactable
{
    [SerializeField] private CanvasGroup puzzleScreen;
    [SerializeField] private GameObject loginPage;
    [SerializeField] private CanvasManager canvasManager;

    private Outline outline;

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
        Debug.Log("Computer clicked!");
        canvasManager.AddToCanvasStack(puzzleScreen);
        loginPage.gameObject.SetActive(true);
       
    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
    }

}
