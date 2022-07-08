using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    private Outline outline;
    private bool state;
    // Subject: Switch
    // Observers: EachLight (which extends from LightObserver)
    private List<LightObserver> observers = new List<LightObserver>();

    private void Start()
    {
          state = true;
    outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public override void OnFocus()
    {
        outline.enabled = true;
    }

    public override void OnInteract()
    {
        Debug.Log("Light clicked!");
        Notify();
    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
    }

    public bool getState()
    {
        return state;
    }

    public void Notify()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].OnNotify();
        }
    }

    public void Subscribe(LightObserver observer)
    {
        observers.Add(observer);
    }
}
