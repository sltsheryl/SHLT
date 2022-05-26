using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
public class Register : MonoBehaviour
{
    public TextMeshProUGUI message;
    public GameObject messageObject;
    public GameObject Email;
    public GameObject Password;
    public Button registerButton;
    private string email;
    private string password;
    public Selectable firstInput;
    EventSystem system;

    void Start()
    {
        messageObject.gameObject.SetActive(false);
        system = EventSystem.current;
        firstInput.Select();
        registerButton.GetComponent<Button>().onClick.AddListener(() => RegisterNow());

    }

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

        else if (Input.GetKeyDown(KeyCode.Return))
        {
            messageObject.gameObject.SetActive(false);
            message.SetText("");
            RegisterNow();
        }


    }

    public void RegisterNow()
    {
        string currScreen = SceneManager.GetActiveScene().name;
        email = Email.GetComponent<TMP_InputField>().text;
        password = Password.GetComponent<TMP_InputField>().text;
        if (email != "" && password != "")
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
