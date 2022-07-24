using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private Inventory inventory;
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
        Debug.Log("Interact with Door");
        if (inventory.Use() is KeyPuzzle)
        {
            Debug.Log("You win!");
        }
    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
    }

}
