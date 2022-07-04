using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EachLight : LightObserver
{
  
    private void Start()
    {
        FPSController.lightSwitch.Suscribe(this);
    }

    public override void OnNotify()
    {
        bool currentLighted = FPSController.lightSwitch.getState();
        if (currentLighted)
        {
            GetComponent<Light>().intensity = 0f;
        } else
        {
            GetComponent<Light>().intensity = 0.8f;
        }
    }
    
}
