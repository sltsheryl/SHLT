using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button submitButton;
    public TextMeshProUGUI message;
    public GameObject messageObject;
    public GameObject Email;
    public GameObject Password;
    public Button registerButton;
    private string email;
    private string password;
    public Selectable firstInput;
    EventSystem system;

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
        email = Email.GetComponent<TMP_InputField>().text;
        password = Password.GetComponent<TMP_InputField>().text;
        if (email != "" && password != "")
        {
           
                Debug.Log("Successful Login!");
            SceneManager.LoadScene("Transition");


        }
        else
        {
            messageObject.gameObject.SetActive(true);
            message.SetText("Invalid Username or Password");
           
        }

    }
}


