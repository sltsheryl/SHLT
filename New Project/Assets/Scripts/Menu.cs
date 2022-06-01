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
    public Button submitButton;
    public TextMeshProUGUI message;
    public GameObject messageObject;
    public TMP_InputField username;
    public TMP_InputField password;
    public Button registerButton;
    public Button forgotPasswordButton;

    public Selectable firstInput;
    EventSystem system;
    Manager manager = new Manager();

    // Start is called before the first frame update
    void Start()
    {
        messageObject.gameObject.SetActive(false);
        system = EventSystem.current;
        firstInput.Select();
        submitButton.GetComponent<Button>().onClick.AddListener(() => Login());
        registerButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("RegisterPage"));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift) || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
            {
                previous.Select();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Tab) || (Input.GetKeyDown(KeyCode.DownArrow)))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }


        //if submit
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            messageObject.gameObject.SetActive(false);
            message.SetText("");
            Login();
        }


    }

    public void Login()
    {
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
                SceneManager.LoadScene("Transition");
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
