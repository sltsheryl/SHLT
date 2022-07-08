using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EachLight : LightObserver
{
    [SerializeField] private Switch lightSwitch;
  
    private void Start()
    {
        lightSwitch.Subscribe(this);
    }

    public override void OnNotify()
    {
        bool currentLighted = lightSwitch.getState();
        if (currentLighted)
        {
            GetComponent<Light>().intensity = 0f;
        } else
        {
            GetComponent<Light>().intensity = 0.8f;
        }
    }
    
}
