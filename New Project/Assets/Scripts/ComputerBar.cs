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
        DateTime thisDay = DateTime.Today;
        date.text = thisDay.ToString();
    }
}
