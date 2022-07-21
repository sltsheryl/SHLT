using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Words : MonoBehaviour
{
    [SerializeField] private bool currentLighted;
    [SerializeField] private Image canvasImage;

    public void ToggleState()
    {
        currentLighted = !currentLighted;
        if (currentLighted)
        {
            canvasImage.gameObject.SetActive(false);
            Debug.Log("Unshow words");

        }
        else
        {
            canvasImage.gameObject.SetActive(true);
            Debug.Log("Show words");
        }
    }
}
