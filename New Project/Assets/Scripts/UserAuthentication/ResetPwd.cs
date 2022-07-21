using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetPwd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField confirmPassword;
    [SerializeField] private Selectable firstInput;

    private FieldSequence fieldSequence;
    private ScramClient scramClient = new ScramClient();

    public void Start()
    {
        fieldSequence = new FieldSequence();
        firstInput.Select();
    }

    public void Update()
    {
        fieldSequence.fieldOrder();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Reset();
        }
    }

    public void Reset()
    {
        string usernameContent = username.text;
        string passwordContent = password.text;
        string password2Content = confirmPassword.text;
        if (usernameContent != "" && passwordContent != "")
        {
            if (passwordContent.Length < 8)
            {
                message.SetText("Choose a longer password!");
            }
            if (!password2Content.Equals(passwordContent))
            {
                message.SetText("Inconsistent password!");
            }
            else
            {
                int status = scramClient.ResetPassword(usernameContent, passwordContent);

                if (status == Constants.RESET_SUCCESSFUL)
                {
                    SceneManager.LoadScene("Menu");
                }
                else if (status == Constants.RESET_FAILED)
                {
                    message.SetText("User not found");
                }
                else if (status == Constants.RESET_SERVER_ERROR)
                {
                    message.SetText("Something went wrong!");
                }
            }
        }
        else
        {
            message.SetText("Invalid Username or Password");

        }
    }
    public void ToLogin()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}



