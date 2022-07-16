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
    [SerializeField] private CanvasGroup nextItem;
    [SerializeField] private TextMeshProUGUI message;
    private FieldSequence fieldSequence;
    [SerializeField] private string correctUsername;
    [SerializeField] private string correctPassword;
    [SerializeField] private Selectable firstInput;
    [SerializeField] private CanvasManager canvasManager;


    private void Start()
    {
        fieldSequence = new FieldSequence();
        firstInput.Select();
        cancel.GetComponent<Button>().onClick.AddListener(() =>
        {
            canvasManager.PopFromCanvasStack();

        });
        login.GetComponent<Button>().onClick.AddListener(() => tryLogin());
    }

    public void Update()
    {
        fieldSequence.fieldOrder();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            tryLogin();
        }
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
        canvasManager.AddToCanvasStack(nextItem);
    }
}
