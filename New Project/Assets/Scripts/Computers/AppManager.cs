using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    

    [SerializeField] private Button decoder;
    [SerializeField] private Button message;
    [SerializeField] private CanvasGroup decoderBrowser;
    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private CanvasGroup messageBrowser;
   

    private void Start()
    {
        
       
        decoder.GetComponent<Button>().onClick.AddListener(() =>
        {
            openDecoder();

        });
        message.GetComponent<Button>().onClick.AddListener(() =>
        {
            openMessage();

        });
    }

    public void openMessage()
    {
        canvasManager.removeCanvas(decoderBrowser);
        canvasManager.AddToCanvasStack(messageBrowser);
    }

    public void openDecoder()
    {
        canvasManager.removeCanvas(messageBrowser);
        canvasManager.AddToCanvasStack(decoderBrowser);
    }

    
    
}
