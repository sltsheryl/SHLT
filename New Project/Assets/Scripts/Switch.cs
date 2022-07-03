using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;
    [SerializeField] private GameObject light3;
    [SerializeField] private GameObject light4;
    [SerializeField] private GameObject light5;
    [SerializeField] private GameObject light6;
    [SerializeField] private GameObject light7;
    [SerializeField] private GameObject light8;
    [SerializeField] private GameObject light9;
    [SerializeField] private GameObject light10;
    [SerializeField] private GameObject light11;
    [SerializeField] private GameObject light12;
    private Outline outline;
    private bool lightsOn;

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
        Debug.Log("Light clicked!");
        if (lightsOn)
        {
            light1.GetComponent<Light>().intensity = 0;
            light2.GetComponent<Light>().intensity = 0;
            light3.GetComponent<Light>().intensity = 0;
            light4.GetComponent<Light>().intensity = 0;
            light5.GetComponent<Light>().intensity = 0;
            light6.GetComponent<Light>().intensity = 0;
            light7.GetComponent<Light>().intensity = 0;
            light8.GetComponent<Light>().intensity = 0;
            light9.GetComponent<Light>().intensity = 0;
            light10.GetComponent<Light>().intensity = 0;
            light11.GetComponent<Light>().intensity = 0;
            light12.GetComponent<Light>().intensity = 0;
        }
        else
        {
            light1.GetComponent<Light>().intensity = 1.5f;
            light2.GetComponent<Light>().intensity = 1.5f;
            light3.GetComponent<Light>().intensity = 1.5f;
            light4.GetComponent<Light>().intensity = 1.5f;
            light5.GetComponent<Light>().intensity = 1.5f;
            light6.GetComponent<Light>().intensity = 1.5f;
            light7.GetComponent<Light>().intensity = 1.5f;
            light8.GetComponent<Light>().intensity = 1.5f;
            light9.GetComponent<Light>().intensity = 1.5f;
            light10.GetComponent<Light>().intensity = 1.5f;
            light11.GetComponent<Light>().intensity = 1.5f;
            light12.GetComponent<Light>().intensity = 1.5f;
        }

    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
    }

}
