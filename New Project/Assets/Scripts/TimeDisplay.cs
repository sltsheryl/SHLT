using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshPro displayTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        System.DateTime theTime = System.DateTime.Now;
        string time = theTime.Hour + ":" + theTime.Minute + ":" + theTime.Second;
        displayTime.SetText(time);
    }
}
