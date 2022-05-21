using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;

public class Main : MonoBehaviour
{

    public Button submitButton;
    //GameObject to select fields from Console
    public TextMeshProUGUI Message;
    public TextMeshProUGUI Success;
    public GameObject Email;
    public GameObject Password;
    private string email;
    private string password;
    //submit pressing enter

    //create a variable so that it will start with an input field
    public Selectable firstInput;
    EventSystem system;
    // Start is called before the first frame update
    void Start()
    {
        //which url element is focused and the order
        system = EventSystem.current;
        firstInput.Select();
        submitButton.GetComponent<Button>().onClick.AddListener(() => Login());
    }

    // Update is called once per frame
    void Update()
    {
        
        //tab or arrow down go next field
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift) || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            //tab key goes next
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
            {
                previous.Select();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Tab) || (Input.GetKeyDown(KeyCode.DownArrow)))
        {
            //tab key goes next
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }


        //if submit
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            submitButton.onClick.Invoke();
            Login();
        }


    }

    public void Login()
    {
        email = Email.GetComponent<InputField>().text;
        password = Password.GetComponent<InputField>().text;
        if (email != "" && password != "")
        {
            Message.text = "";
            Success.text = "Login Successful!";
            Success.color = new Color(0, 1, 0, 1);

        }
        //to-do: do checking whether it tallies
        else
        {
            Message.text = "Invalid username or password!";
            Message.color = new Color(1, 0, 0, 1);
        }

    }
}


