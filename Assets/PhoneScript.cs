using UnityEngine;

public class PhoneScript : MonoBehaviour
{
    public AudioClip ringSound; // The initial sound to play when the phone rings
    public AudioClip newClip; // The new sound to play after collision

    private AudioSource audioSource;
    private bool isRinging;
    private bool isPlayingNewClip;

    private void Start()
    {
        InitializeAudioSource();
    }

    private void InitializeAudioSource()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true; // Loop the ring sound
        audioSource.clip = ringSound;
    }

    // Method to start the ringing sound
    public void StartRing()
    {
        if (!isRinging)
        {
            InitializeAudioSource(); // Ensure the AudioSource is initialized
            audioSource.Play();
            isRinging = true;
            isPlayingNewClip = false;
        }
    }

    // Method to stop the ringing sound
    public void StopRing()
    {
        if (isRinging)
        {
            audioSource.Stop();
            isRinging = false;
        }
    }

    // Check if the phone is currently ringing
    public bool IsRinging()
    {
        return isRinging;
    }

    // Method to play the new audio clip
    public void PlayNewClip()
    {
        // Stop the current clip and set the new one
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.PlayOneShot(newClip);
    }

}
