using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    [SerializeField] private Button decoder;
    [SerializeField] private Button message;
    [SerializeField] private CanvasGroup decoderBrowser;
    [SerializeField] private CanvasGroup messageBrowser;
    private bool Messagezoomed;
    private bool Decoderzoomed;

    private void Start()
    {
        Messagezoomed = false;
        Decoderzoomed = false;
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
        decoderBrowser.gameObject.SetActive(false);

        messageBrowser.gameObject.SetActive(true);
    }

    public void openDecoder()
    {
        messageBrowser.gameObject.SetActive(false);

        decoderBrowser.gameObject.SetActive(true);
    }

    public void closeMessage()
    {
        messageBrowser.gameObject.SetActive(false);
    }

    public void closeDecoder()
    {
        decoderBrowser.gameObject.SetActive(false);
    }

    public void resizeDecoder()
    {
        if (!Decoderzoomed)
        {
            decoderBrowser.transform.localScale = new Vector3(2.4f, 2.4f, 1);
            Decoderzoomed = true;
        }
        else
        {
            decoderBrowser.transform.localScale = new Vector3(2f, 2f, 1);
            Decoderzoomed = false;
        }
    }

    public void resizeMessage()
    {
        if (!Messagezoomed)
        {
            messageBrowser.transform.localScale = new Vector3(2f, 2f, 1);
            Messagezoomed = true;
        }
        else
        {
            messageBrowser.transform.localScale = new Vector3(1.8f, 1.8f, 1);
            Messagezoomed = false;
        }
    }
}
