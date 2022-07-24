using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManagerLB : MonoBehaviour
{

    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private Button finder;
    [SerializeField] private CanvasGroup finderBrowser;

    private void Start()
    {

        finder.GetComponent<Button>().onClick.AddListener(() =>
        {
            openFinder();

        });
    }

    public void openFinder()
    {
        canvasManager.AddToCanvasStack(finderBrowser);
    }


}
