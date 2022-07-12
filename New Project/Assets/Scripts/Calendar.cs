using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Calendar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI todayDate;

    private void Start()
    {
        System.DateTime theTime = System.DateTime.Now;
        string day = theTime.Day.ToString();
        string month = theTime.Month.ToString();
        string year = theTime.Year.ToString();
        todayDate.text = day + "/" + month + "/" + year;
    }

   
}
