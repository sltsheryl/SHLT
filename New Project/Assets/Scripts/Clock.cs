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
        time.text = DateTime.Now.ToString("HH: mm: ss");

        date.text = "8/8/22"; // theTime.Date.ToString("d");
        
    }
}
