using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeButtons : MonoBehaviour
{
    private bool browserZoomed;

    [SerializeField] private CanvasGroup currentBrowser;
    [SerializeField] CanvasManager canvasManager;

    void Start()
    {
        browserZoomed = false;
       
    }

    public void resizeBrowser()
    {
        if (!browserZoomed)
        {
            currentBrowser.transform.localScale = new Vector3(2.3f, 2.3f, 1);
            browserZoomed = true;
        }
        else
        {
            currentBrowser.transform.localScale = new Vector3(2f, 2f, 1);
            browserZoomed = false;
        }
    }


    public void closeBrowser()
    {
        canvasManager.removeCanvas(currentBrowser);
    }


}
