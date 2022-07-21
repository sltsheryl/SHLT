using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class RegisterScreen : MonoBehaviour
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
            Register();
        }
    }

    public void Register()
    {
        string usernameContent = username.text;
        string passwordContent = password.text;
        if (usernameContent != "" && passwordContent != "")
        {
            if (passwordContent.Length < 8)
            {
                message.SetText("Choose a longer password!");
            }
            else
            {
                int status = scramClient.RegisterUser(usernameContent, passwordContent);

                if (status == Constants.REGISTER_SUCCESSFUL)
                {
                    ToLogin();
                }
                else if (status == Constants.REGISTER_FAILED)
                {
                    message.SetText("Invalid Username or Password!");
                }
                else if (status == Constants.REGISTER_SERVER_ERROR)
                {
                    message.SetText("Something went wrong!");
                }
            }
        }

        else
        {
            message.SetText("Invalid Username or Password!");

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


