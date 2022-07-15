using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    List<CanvasGroup> canvasList = new List<CanvasGroup>();
    [SerializeField] private CanvasGroup pauseMenu;

    public void AddToCanvasStack(CanvasGroup canvas)
    {
        canvas.gameObject.SetActive(true);
        canvasList.Add(canvas);
        FPSController.DisableControls();
    }

    public void PopFromCanvasStack()
    {
        CanvasGroup lastCanvas = canvasList[canvasList.Count - 1];
        canvasList.RemoveAt(canvasList.Count - 1);  // Assume stack is non-empty
        lastCanvas.gameObject.SetActive(false);
        if (canvasList.Count == 0)
        {
            FPSController.EnableControls();
        }
    }

    public void PopTillEmpty()
    {
        while (canvasList.Count > 0)
        {
            CanvasGroup lastCanvas = canvasList[canvasList.Count - 1];
            canvasList.RemoveAt(canvasList.Count - 1);  // Assume stack is non-empty
            lastCanvas.gameObject.SetActive(false);
        }
        FPSController.EnableControls();
    }

    public void removeCanvas(CanvasGroup canvas)
    {
        if (canvasList.Contains(canvas)){
            canvas.gameObject.SetActive(false);
            canvasList.Remove(canvas);
        }
        if (canvasList.Count == 0)
        {
            FPSController.EnableControls();
        }
    }

    void Update()
    {
        Debug.Log(canvasList.Count);
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
                AddToCanvasStack(pauseMenu);
            }
        }
    }
}
