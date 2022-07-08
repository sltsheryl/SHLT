using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CipherSlider : MonoBehaviour
{
    [SerializeField] private Slider keySlider;
    [SerializeField] private TextMeshProUGUI key;
    public void Update()
    {
        float keyValue = keySlider.value;
        key.text = keyValue.ToString();
    }


}