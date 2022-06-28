using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SQLClient;

public class ResetPwd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private GameObject messageObject;
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField confirmPassword;
    [SerializeField] private Button reset;
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
        reset.GetComponent<Button>().onClick.AddListener(() => Reset());
        backButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("Menu"));
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
        Debug.Log(username);
        string usernameContent = username.text;
        string passwordContent = password.text;
        string password2Content = confirmPassword.text;
        if (usernameContent != "" && passwordContent != "")
        {
            if (passwordContent.Length < 8)
            {
                messageObject.gameObject.SetActive(true);
                message.SetText("Choose a longer password!");
            }
            if (!password2Content.Equals(passwordContent))
            {
                messageObject.gameObject.SetActive(true);
                message.SetText("Inconsistent password!");
            }
            else
            {
                User user = new User(usernameContent, passwordContent);
                bool userModified = manager.ModifyUser(user);

                if (userModified)
                {
                    Debug.Log("Successful Change!");
                    SceneManager.LoadScene("Menu");
                }
                else
                {
                    messageObject.gameObject.SetActive(true);
                    message.SetText("User not found");
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



