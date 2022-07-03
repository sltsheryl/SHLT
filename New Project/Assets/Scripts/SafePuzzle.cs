using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;


public class SafePuzzle : Safe
{
    private string correctPassword = "123";
    private string input;
    [SerializeField] private TextMeshProUGUI result;
    [Header("buttons")]
    [SerializeField] private Button one;
    [SerializeField] private Button two;
    [SerializeField] private Button three;
    [SerializeField] private Button four;
    [SerializeField] private Button five;
    [SerializeField] private Button six;
    [SerializeField] private Button seven;
    [SerializeField] private Button eight;
    [SerializeField] private Button nine;
    [SerializeField] private Button zero;
    [SerializeField] private Button Reset;
    [SerializeField] private Button Enter;

    private void Start()
    {
        one.onClick.AddListener(() => input += "1");
        two.onClick.AddListener(() => input += "2");
        three.onClick.AddListener(() => input += "3");
        four.onClick.AddListener(() => input += "4");
        five.onClick.AddListener(() => input += "5");
        six.onClick.AddListener(() => input += "6");
        seven.onClick.AddListener(() => input += "7");
        eight.onClick.AddListener(() => input += "8");
        nine.onClick.AddListener(() => input += "9");
        Reset.onClick.AddListener(() => input = "");
        Enter.onClick.AddListener(() =>
        {
            if (input.Equals(correctPassword))
            {
                open();
            }
            else
            {
                wrongPassword();
                Invoke("eraseMessage", 2f); 
            }
        }
        );
    }
   
 
private void open()
{
    result.text = "Correct";
    result.color = Color.green;
        openSafe();
    }
    private void wrongPassword()
    {
        Debug.Log("Wrong pin!");
        input = "";
        result.text = "Incorrect";
        result.color = Color.red;
    }
    private void eraseMessage()
    {
        result.text = "";
    }


}
