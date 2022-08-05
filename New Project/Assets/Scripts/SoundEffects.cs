using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    [SerializeField] private AudioSource footstepAudioSource = default;
    [SerializeField] private AudioClip groundAudio = default;
    [SerializeField] private AudioClip jumpAudio = default;

    private KeyCode jumpKey = KeyCode.Space;
    private bool stop;
    //private float footstepTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        stop = false;
    }

    private void WalkSound()
    {
       
        if (!footstepAudioSource.isPlaying && !stop)
        {
            footstepAudioSource.PlayOneShot(groundAudio);
        }
        if (character.velocity.magnitude == 0)
        {
            stop = true;
            footstepAudioSource.Stop();
        }
        //}
    }

    private void JumpSound()
    {
            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.PlayOneShot(jumpAudio);
            }
        
    }

    private void Update()
    {
        if (character.isGrounded && character.velocity.magnitude > 0f && !Input.GetKey(jumpKey))
        {
            WalkSound();
        }
        if (Input.GetKey(jumpKey) && character.isGrounded)
        {
            JumpSound();
        }

    }

}
