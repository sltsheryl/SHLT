using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ComputerBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI date;
    [SerializeField] private TextMeshProUGUI user;
    
    private void Start()
    {
        user.text = "admin";
    }

    private void Update()
    {
        System.DateTime theTime = System.DateTime.Now;
        date.text = theTime.ToString();
    }
}
