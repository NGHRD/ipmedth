using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public SpotlightController spotlightController;

    private AudioSource audioSource;
    private bool isPlaying = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found. Please attach an AudioSource to the same GameObject.");
        }
    }

    public void PlayAudioAndSwitchTarget(AudioClip audioClip)
    {
        if (!isPlaying && audioClip != null && audioSource != null)
        {
            // Set the AudioClip to the AudioSource
            audioSource.clip = audioClip;

            // Subscribe to the audio finished event
            audioSource.PlayOneShot(audioClip);
            Invoke("SwitchToNextTarget", audioClip.length);

            // Set the flag to indicate that audio is playing
            isPlaying = true;
        }
    }

    private void SwitchToNextTarget()
    {
        if (spotlightController != null)
        {
            spotlightController.SwitchToNextTarget();

            // Reset the flag when the audio finishes playing and the target is switched
            isPlaying = false;
        }
        else
        {
            Debug.LogError("SpotlightController is not set. Please assign a SpotlightController to the script.");
        }
    }
}