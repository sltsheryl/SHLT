using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;

public class KeyPuzzle : Interactable
{
    [SerializeField] private CanvasGroup puzzleScreen;

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
        Debug.Log("Key clicked!");
        puzzleScreen.gameObject.SetActive(true);
        FPSController.CanMove = false;
    }


    public override void OnLoseFocus()
    {
        outline.enabled = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            puzzleScreen.gameObject.SetActive(false);
            FPSController.CanMove = true;

        }
    }
}
