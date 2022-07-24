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
    [SerializeField] private string username;
    
    private void Start()
    {
        user.text = username;
    }

    private void Update()
    {
        date.text = DateTime.Now.ToString();
    }
}
