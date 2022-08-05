using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSound : MonoBehaviour
{
    [SerializeField] private AudioSource soundAudioSource = default;
    [SerializeField] private AudioClip keypadAudio = default;
    

    public void PlayPinSound()
    {

        soundAudioSource.PlayOneShot(keypadAudio);
            
    }

   
}
