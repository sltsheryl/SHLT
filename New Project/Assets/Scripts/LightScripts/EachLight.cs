using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EachLight : MonoBehaviour
{
    [SerializeField] private Switch lightSwitch; 
    private LightObserver lightObserver;
  
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
        } else
        {
            GetComponent<Light>().intensity = 0f;
        }
    }
    
}
