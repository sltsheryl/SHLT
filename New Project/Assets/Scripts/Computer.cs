using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Interactable
{
    [SerializeField] private CanvasGroup puzzleScreen;
    [SerializeField] private CanvasGroup loginPage;

    private Outline outline;

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
        Debug.Log("Computer clicked!");
        puzzleScreen.gameObject.SetActive(true);
        loginPage.gameObject.SetActive(true);
        FPSController.CanMove = false;
    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
        puzzleScreen.gameObject.SetActive(false);
    }

}
