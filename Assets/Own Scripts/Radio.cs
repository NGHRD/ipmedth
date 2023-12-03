using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    private int currentStation = 0;
    private bool isMuted = false;

    // Array of AudioClip for different stations
    public AudioClip[] stations;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeStation()
    {
        if (stations.Length <= 1) return; // Also return if there's only one station

        // Save the current station index
        int previousStation = currentStation;

        do
        {
            // Increment and wrap the station index
            currentStation = (currentStation + 1) % stations.Length;
        } while (currentStation == previousStation && stations.Length > 1); // Repeat until a different station is found

        // Set and play the new station
        audioSource.clip = stations[currentStation];
        audioSource.Play();
    }


    public void ToggleMute()
    {

        isMuted = !isMuted;
        audioSource.mute = isMuted;
    }
}
