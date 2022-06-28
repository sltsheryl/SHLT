using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using SQLClient;


public class Register : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private GameObject messageObject;
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private Button registerButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Selectable firstInput;
    private FieldSequence fieldSequence;
    private EventSystem system;
    private Manager manager = new Manager();

    public void Start()
    {
        fieldSequence = new FieldSequence();
        messageObject.gameObject.SetActive(false);
        system = EventSystem.current;
        firstInput.Select();
        registerButton.GetComponent<Button>().onClick.AddListener(() => RegisterNow());
        backButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Menu"));
    }

    public void Update()
    {
        fieldSequence.fieldOrder();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RegisterNow();
        }
    }

    public void RegisterNow()
    {
        Debug.Log(username);
        string usernameContent = username.text;
        string passwordContent = password.text;
        if (usernameContent != "" && passwordContent != "")
        {
            if (passwordContent.Length < 8)
            {
                messageObject.gameObject.SetActive(true);
                message.SetText("Choose a longer password!");
            }
            else
            {
                User user = new User(usernameContent, passwordContent);
                bool userCreated = manager.CreateUser(user);

                if (userCreated)
                {
                    Debug.Log("Successful Registration!");
                    SceneManager.LoadScene("Menu");
                }
                else
                {
                    messageObject.gameObject.SetActive(true);
                    message.SetText("Invalid Username or Password");
                }

            }
        }

        else
        {
            messageObject.gameObject.SetActive(true);
            message.SetText("Invalid Username or Password");

        }

    }
}


