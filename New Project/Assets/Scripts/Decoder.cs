using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Decoder : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private Slider keySlider;
    [SerializeField] private Button decrypt;
    [SerializeField] private TextMeshProUGUI output;
    [SerializeField] private CanvasGroup puzzleScreen;

    int key;
    private void Start()
    {
        key = (int) keySlider.value;
        decrypt.GetComponent<Button>().onClick.AddListener(() => setOutput());
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            puzzleScreen.gameObject.SetActive(false);
            FPSController.CanMove = true;
        }


    }

    private void setOutput()
    {
        output.text = Decrypt(input.text, (int)keySlider.value);
    }

    private string Decrypt(string plainText, int caesarKey)
    {
        string cipherText = "";
        for (int i = 0; i < plainText.Length; i++)
        {
            if (plainText[i] == ' ')
            {
                cipherText += plainText[i];
            }
            else if (plainText[i] == ',')
            {
                cipherText += plainText[i];
            }
            else if (plainText[i] == '.')
            {
                cipherText += plainText[i];
            }
            else
            {
                int currentAscii = (int)plainText[i];
                int newAscii = currentAscii - caesarKey;
                int correctAscii = newAscii;
                if (newAscii - 'a' < 0)
                {
                    int remainder = 97 % newAscii;
                    correctAscii = 122 - remainder + 1;

                }
                cipherText += (char)correctAscii;
            }
            
        }
        
        return cipherText;
    }
}
