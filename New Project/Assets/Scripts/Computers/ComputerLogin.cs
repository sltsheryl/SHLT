using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComputerLogin : MonoBehaviour
{
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private Button login;
    [SerializeField] private Button cancel;
    [SerializeField] private CanvasGroup puzzleScreen;
    [SerializeField] private CanvasGroup loginPage;
    [SerializeField] private CanvasGroup computerPage;
    [SerializeField] private CanvasGroup caesarMessage;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private string correctUsername;
    [SerializeField] private string correctPassword;
    [SerializeField] private CanvasManager canvasManager;


    private void Start()
    {
        cancel.GetComponent<Button>().onClick.AddListener(() =>
        {
            canvasManager.PopTillEmpty();

        });
        login.GetComponent<Button>().onClick.AddListener(() => tryLogin());
    }

    
    private void tryLogin()
    {
        if (username.text.Equals(correctUsername) && password.text.Equals(correctPassword))
        {
            canvasManager.removeCanvas(loginPage);
            canvasManager.AddToCanvasStack(computerPage);
            Invoke("showMessage", 2f);
        } else
        {
            message.text = "Incorrect";
        }
    }

    private void showMessage()
    {
        canvasManager.AddToCanvasStack(caesarMessage);
    }
}
