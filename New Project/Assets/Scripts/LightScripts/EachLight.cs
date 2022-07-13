using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EachLight : MonoBehaviour
{
    [SerializeField] private Switch lightSwitch; 
    private LightObserver lightObserver;
    [SerializeField] private Material offLightMaterial;
    [SerializeField] private Material onLightMaterial;

    private void Start()
    {
        LightSubject lightSubject = lightSwitch.GetLightSubject();
        lightObserver = new LightObserver(this, lightSubject);
    }

    public void UpdateState(bool state)
    {
        bool currentLighted = state;
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
