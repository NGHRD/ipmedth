using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Radio_oranje : MonoBehaviour
{
    // Array of AudioClip for different stations
    private AudioSource audioSource;
    public bool alreadyPlayed = false;
    public AudioClip clip;
    public bool hasSwitchedToNextTarget = false;
    public SpotlightController spotlightController;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playRadioOranje()
    {
        if (!alreadyPlayed)
        {
            audioSource.PlayOneShot(clip, 0.7F);
            alreadyPlayed = true;
            if (!audioSource.isPlaying && !hasSwitchedToNextTarget)
            {
                spotlightController.SwitchToNextTarget();
                hasSwitchedToNextTarget=true;
            }
        }


    }
}
