using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;

public class KeyPuzzle : Interactable
{
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
    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
    }
}
