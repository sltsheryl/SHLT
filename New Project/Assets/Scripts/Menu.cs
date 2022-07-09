using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using SQLClient;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button submitButton;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private GameObject messageObject;
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private Button registerButton;
    [SerializeField] private Button forgotPasswordButton;
    [SerializeField] private Selectable firstInput;
    [SerializeField] private Button quitApp;

    private FieldSequence fieldSequence;
    private EventSystem system;
    private Manager manager = new Manager();

    public void Start()
    {
        fieldSequence = new FieldSequence();
        messageObject.gameObject.SetActive(false);
        system = EventSystem.current;
        firstInput.Select();
        submitButton.GetComponent<Button>().onClick.AddListener(() => Login());
        registerButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("RegisterPage"));
        forgotPasswordButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("ResetPassword"));
        quitApp.GetComponent<Button>().onClick.AddListener(() => Application.Quit());

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
        fieldSequence = new FieldSequence();
        string currScreen = SceneManager.GetActiveScene().name;
        string usernameContent = username.text;
        string passwordContent = password.text;
        if (usernameContent != "" && passwordContent != "")
        {
            User user = new User(usernameContent, passwordContent);
            int userFound = manager.GetUser(user);

            if (userFound == 0)
            {
                Debug.Log("Successful Login!");
                SceneManager.LoadScene("StartGame");
            }
            else if (userFound == 1)
            {
                messageObject.gameObject.SetActive(true);
                message.SetText("User not found!");
            }
            else if (userFound == 2)
            {
                messageObject.gameObject.SetActive(true);
                message.SetText("Wrong password!");
            }
            else
            {
                messageObject.gameObject.SetActive(true);
                message.SetText("Error!");
            }
        }
        else
        {
            messageObject.gameObject.SetActive(true);
            message.SetText("Cannot leave blank!");

        }

    }
}
