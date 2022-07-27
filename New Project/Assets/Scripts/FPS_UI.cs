using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS_UI : MonoBehaviour
{
    [SerializeField] private Image inventorySprite;
    [SerializeField] private Text messageText;
    [SerializeField] private float messageTime;

    // Start is called before the first frame update
    void Start()
    {
        messageText.text = "";
    }

    public void SetImage(Sprite image)
    {
        if (image == null)
        {
            inventorySprite.color = new Color(255, 255, 255, 0);
        }
        else
        {
            inventorySprite.color = Color.white;
        }
        inventorySprite.sprite = image;
    }

    public void SetMessageText(string text)
    {
        messageText.text = text;
        Invoke("DisableMessageText", messageTime);
    }

    public void DisableMessageText()
    {
        messageText.text = "";
    }
}
