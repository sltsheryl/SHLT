using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
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
            Login();
        }
    }

    public void Login()
    {
        string usernameContent = username.text;
        string passwordContent = password.text;
        if (usernameContent != "" && passwordContent != "")
        {
            int status = scramClient.LogIn(usernameContent, passwordContent);

            if (status == Constants.LOGIN_SUCCESSFUL)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else if (status == Constants.LOGIN_FAILED)
            {
                message.SetText("Authentication Failed!");
            }
            else if (status == Constants.LOGIN_FAILED)
            {
                message.SetText("Something went wrong!");
            }
        }
        else
        {
            message.SetText("Cannot leave blank!");

        }
    }

    public void ToRegister()
    {
        SceneManager.LoadScene("RegisterPage");
    }

    public void ToReset()
    {
        SceneManager.LoadScene("ResetPassword");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
