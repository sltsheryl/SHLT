using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    List<CanvasGroup> canvasList = new List<CanvasGroup>();
    [SerializeField] private CanvasGroup pauseMenu;

    public void AddToCanvasStack(CanvasGroup canvas)
    {
        if (canvasList.Count == 0)
        {
            FPSController.DisableControls();
        }
        canvasList.Add(canvas);
    }

    public void PopFromCanvasStack()
    {
        CanvasGroup lastCanvas = canvasList[canvasList.Count - 1];
        canvasList.RemoveAt(canvasList.Count - 1);  // Assume stack is non-empty
        lastCanvas.gameObject.SetActive(false);
        FPSController.EnableControls();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Escape pressed!" + " " + canvasList.Count);
            if (canvasList.Count > 0)
            {
                PopFromCanvasStack();
                if (canvasList.Count == 0)
                {
                    FPSController.EnableControls();
                }
            }
            else
            {
                // pull up the pause menu
                pauseMenu.gameObject.SetActive(true);
                AddToCanvasStack(pauseMenu);
            }
        }
    }
}
