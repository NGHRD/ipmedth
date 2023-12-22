using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] audioClips;  // Array of audio clips to be played
    public AudioSource audioSource;

    public void PlayAudioAtTargetIndex(int targetIndex)
    {
        // Check if the target index is within the bounds of the audioClips array
        if (targetIndex >= 0 && targetIndex < audioClips.Length)
        {
            // Play the corresponding audio clip
            audioSource.clip = audioClips[targetIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Target index out of bounds or no corresponding audio clip found.");
        }
    }
}
