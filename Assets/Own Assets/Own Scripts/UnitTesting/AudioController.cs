using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] audioClips;  // Array of audio clips to be played
    public AudioSource audioSource;

    private int currentClipIndex = 0; // Keep track of the currently playing clip index
    private SpotlightController spotlightController;
    private MoveObjectsToGameEnd moveObjectsToGameEnd;

    private void Start()
    {
        spotlightController = GetComponent<SpotlightController>();
        moveObjectsToGameEnd = GetComponent<MoveObjectsToGameEnd>();
    }

    public void PlayAudioAtTargetIndex(int targetIndex)
    {
        // Check if the target index is within the bounds of the audioClips array
        if (targetIndex >= 0 && targetIndex < audioClips.Length)
        {
            // Play the corresponding audio clip
            audioSource.clip = audioClips[targetIndex];
            audioSource.Play();
            currentClipIndex = targetIndex; // Update the current clip index

            // Invoke the function when the audio clip finishes playing
            Invoke("OnAudioClipEnd", audioSource.clip.length);
        }
        else
        {
            Debug.LogWarning("Target index out of bounds or no corresponding audio clip found.");
        }
    }

    // This is the function to execute when the audio clip ends
    private void OnAudioClipEnd()
    {
        // Check if we've reached the end of the audio clip list
        if (currentClipIndex == audioClips.Length - 1)
        {
            // Execute your function here when the list ends
            OnAudioListEnd();
        }
    }

    // This is the function to execute when the audio list ends
    private void OnAudioListEnd()
    {
        // Implement your logic here
        Debug.Log("Audio list has ended!");
        moveObjectsToGameEnd.MovePlayersToNewPosition(moveObjectsToGameEnd.position);
    }
}
