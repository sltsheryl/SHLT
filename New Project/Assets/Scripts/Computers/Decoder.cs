using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Decoder : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Slider keySlider;
    [SerializeField] private TextMeshProUGUI output;
    [SerializeField] private CanvasGroup puzzleScreen;

    int key;
    private void Start()
    {
        key = (int) keySlider.value;
        
    }


    public void setOutput()
    {
        output.text = Decrypt(input.text, (int)keySlider.value);
    }

    private string Decrypt(string plainText, int caesarKey)
    {
        caesarKey = -caesarKey;
        string cipherText = "";
        for (int i = 0; i < plainText.Length; i++)
        {
            char alphabet = plainText[i];
            if (alphabet >= 'a' && alphabet <= 'z')
            {
                alphabet = (char)(alphabet + caesarKey);
                if (alphabet > 'z')
                {
                    alphabet = (char)(alphabet + 'a' - 'z' - 1);
                }
                cipherText = cipherText + alphabet;
            }
            else if (alphabet >= 'A' && alphabet <= 'Z')
            {
                alphabet = (char)(alphabet + caesarKey);
                if (alphabet > 'Z')
                {
                    alphabet = (char)(alphabet + 'A' - 'Z' - 1);
                }
                cipherText += alphabet;
            }
            else
            {
                cipherText += alphabet;
            }
        }

        return cipherText;
    }
}
