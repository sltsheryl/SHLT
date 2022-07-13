using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;

public class KeyPuzzle : Interactable
{
    [SerializeField] private CanvasGroup puzzleScreen;
    [SerializeField] private CanvasManager canvasManager;

    private int pinMask = 0;
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
}