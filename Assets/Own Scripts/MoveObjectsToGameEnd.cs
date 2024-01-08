using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class MoveObjectsToGameEnd : MonoBehaviour
{
    public GameObject position;
    public AudioSource audioSource;
    public AudioClip clip;

    private bool isPlaying = false;

    public void MovePlayersToNewPosition(GameObject newPosition)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            player.transform.position = newPosition.transform.position;
        }

        if (!isPlaying && clip != null && audioSource != null)
        {
            // Set the AudioClip to the AudioSource
            audioSource.clip = clip;

            // Subscribe to the audio finished event
            audioSource.PlayOneShot(clip);

            // Set the flag to indicate that audio is playing
            isPlaying = true;
        }
    }
}
