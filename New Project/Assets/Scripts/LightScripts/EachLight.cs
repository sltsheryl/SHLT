using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EachLight : MonoBehaviour
{
    [SerializeField] private Material offLightMaterial;
    [SerializeField] private Material onLightMaterial;

    [SerializeField] private bool currentLighted;

    public void ToggleState()
    {
        currentLighted = !currentLighted;
        if (currentLighted)
        {
            GetComponent<Light>().intensity = 0.8f;
            foreach (Transform child in transform)
            {
                child.GetComponent<MeshRenderer>().material = onLightMaterial;
            }
        } else
        {
            GetComponent<Light>().intensity = 0f;
            foreach (Transform child in transform)
            {
                child.GetComponent<MeshRenderer>().material = offLightMaterial;
            }
        }
    }
    
}
