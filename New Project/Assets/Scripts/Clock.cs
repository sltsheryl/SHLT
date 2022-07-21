using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI date;

    void Update()
    {
        System.DateTime theTime = System.DateTime.Now;
        int currMinute = theTime.Minute;
        int currHour = theTime.Hour;
        int currSecond = theTime.Second;
        time.text = currHour + ": " + currMinute + ": " + currSecond;

        date.text = theTime.Date.ToString("d");
        
    }
}
