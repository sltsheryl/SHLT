using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SafePuzzle : MonoBehaviour
{
    [SerializeField] private string correctPassword;
    private string input;
    [SerializeField] private TextMeshProUGUI result;
    [SerializeField] private Safe safe;

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
        safe.openSafe();
        safe.gameObject.layer = 2;
        safe.OnLoseFocus();
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
