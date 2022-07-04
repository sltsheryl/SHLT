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

  public void onClicked(Button button)
    {
        if (button.name == "Enter")
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
        } else if (button.name == "R")
        {
            input = "";
        } else
        {
            input += button.name;
        }
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
