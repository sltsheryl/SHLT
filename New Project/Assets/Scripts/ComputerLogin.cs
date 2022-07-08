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


    private void Start()
    {
        cancel.GetComponent<Button>().onClick.AddListener(() =>
        {
            puzzleScreen.gameObject.SetActive(false);
            FPSController.CanMove = true;

        });
        login.GetComponent<Button>().onClick.AddListener(() => tryLogin());
    }

    
    private void tryLogin()
    {
        if (username.text == "admin" && password.text == "admin")
        {
            loginPage.gameObject.SetActive(false);
            computerPage.gameObject.SetActive(true);
            Invoke("showMessage", 2f);
        } else
        {
            message.text = "Incorrect";
        }
    }

    private void showMessage()
    {
        caesarMessage.gameObject.SetActive(true);
    }
}
