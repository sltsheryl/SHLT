using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    private Outline outline;
    private bool state = true;
    private LightSubject lightSubject;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;

        lightSubject = GetComponent<LightSubject>();
    }

    public override void OnFocus()
    {
        outline.enabled = true;
    }

    public override void OnInteract()
    {
        Debug.Log("Light clicked!");
        ToggleState();
        lightSubject.DispatchNotifications(state);
    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
    }

    private void ToggleState()
    {
        state = !state;
    }
}
