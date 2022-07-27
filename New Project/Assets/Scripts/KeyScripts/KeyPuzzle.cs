using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class KeyPuzzle : Takeable
{
    [SerializeField] private CanvasGroup puzzleScreen;
    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private Inventory inventory;
    [SerializeField] private FPS_UI fpsUi;

    private int pinMask = 0;
    private int correctPins = (1 << 4) ^ (1 << 6) ^ (1 << 7) ^ (1 << 10);
    [SerializeField] private GameObject pin1;
    [SerializeField] private GameObject pin2;
    [SerializeField] private GameObject pin3;
    [SerializeField] private GameObject pin4;
    [SerializeField] private GameObject pin5;
    [SerializeField] private GameObject pin6;
    [SerializeField] private GameObject pin7;
    [SerializeField] private GameObject pin8;
    [SerializeField] private GameObject pin9;
    [SerializeField] private GameObject pin10;
    [SerializeField] private GameObject pin11;
    GameObject[] pins;

    private Outline outline;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;

        pins = new GameObject[] { pin1, pin2, pin3, pin4, pin5, pin6, pin7, pin8, pin9, pin10, pin11 };
    }

    public override void OnFocus()
    {
        outline.enabled = true;
    }

    public override void OnInteract()
    {
        Debug.Log("Key clicked!");
        puzzleScreen.gameObject.SetActive(true);
        canvasManager.AddToCanvasStack(puzzleScreen);
    }

    public override void OnLoseFocus()
    {
        outline.enabled = false;
    }

    public void TogglePin(int pinNum)
    {
        pinMask ^= 1 << pinNum;
        pins[pinNum - 1].SetActive(!pins[pinNum - 1].activeSelf);
    }

    public override void OnTake()
    {
        if (pinMask == correctPins)
        {
            inventory.Take(this);
            gameObject.SetActive(false);
        } 
        else
        {
            fpsUi.SetMessageText("You can only pick this up when the pins are in correct configuration");
        }
    }

   
}
