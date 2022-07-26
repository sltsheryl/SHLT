using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComputerLogin : MonoBehaviour
{
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private CanvasGroup puzzleScreen;
    [SerializeField] private GameObject loginPage;
    [SerializeField] private GameObject computerPage;
    [SerializeField] private GameObject nextItem;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private string correctUsername;
    [SerializeField] private string correctPassword;
    [SerializeField] private Selectable firstInput;
    private FieldSequence fieldSequence;
    [SerializeField] private CanvasManager canvasManager;

    private void Start()
    {
        fieldSequence = ScriptableObject.CreateInstance<FieldSequence>();

        firstInput.Select();
       
    }

    public void Update()
    {
        fieldSequence.fieldOrder();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            tryLogin();
        }
    }

    public void tryLogin()
    {
        if (username.text.Equals(correctUsername) && password.text.Equals(correctPassword))
        {
            loginPage.gameObject.SetActive(false);
            computerPage.gameObject.SetActive(true);
            Invoke("showMessage", 3f);

        } else
        {
            message.text = "Incorrect";
            Invoke("eraseMessage", 3f);
        }
    }

    public void closeComputer()
    {
        canvasManager.PopFromCanvasStack();
        puzzleScreen.gameObject.SetActive(false);
    }

    private void showMessage()
    {
        nextItem.gameObject.SetActive(true);
    }

    private void eraseMessage()
    {
        message.text = "";
    }
}
