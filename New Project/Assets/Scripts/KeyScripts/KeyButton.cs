using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{
    [SerializeField] KeyPuzzle manager;
    [SerializeField] int buttonNumber;

    void Start()
    {
        GetComponentInChildren<Text>().text = buttonNumber.ToString();
    }

    public void OnClick()
    {
        Debug.Log("Button " + buttonNumber + " Pressed!");
        manager.TogglePin(buttonNumber);
    }
}
