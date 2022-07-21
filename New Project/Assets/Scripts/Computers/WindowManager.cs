using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{   
    private bool magnified;
   [SerializeField] private GameObject currentWindow;
    [SerializeField] private List<GameObject> otherWindows;
    private void Start()
    {
        magnified = false;
        
    }
    public void closeWindow()
    {
       
        currentWindow.gameObject.SetActive(false);
    }

    public void openWindow()
    {
        if (otherWindows != null)
        {
            foreach (GameObject go in otherWindows)
            {
                go.gameObject.SetActive(false);

            }
        }
        currentWindow.gameObject.SetActive(true);
    }

    public void resizeWindow()
    {
        if (magnified)
        {
            currentWindow.gameObject.transform.localScale = new Vector3(0.23f, 0.08f, 1);
            magnified = !magnified;

        }
        else
        {
            currentWindow.gameObject.transform.localScale = new Vector3(0.33f, 0.1f, 1);
            magnified = !magnified;
        }
    }

}
