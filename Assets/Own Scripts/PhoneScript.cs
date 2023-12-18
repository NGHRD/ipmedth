using Fusion.XR.Shared.Grabbing.NetworkHandColliderBased;
using UnityEngine;

public class PhoneScript : MonoBehaviour
{
    public AudioClip ringSound; // The initial sound to play when the phone rings
    public AudioClip newClip; // The new sound to play after collision
    public SpotlightController spotlightController;

    private NetworkHandColliderGrabbable grabbable;
    private AudioSource audioSource;
    private bool isRinging;
    public bool isPlayingNewClip = false;
    private bool hasSwitchedTarget; // Flag to keep track of target switch

    private void Start()
    {
        grabbable = GetComponent<NetworkHandColliderGrabbable>();
        InitializeAudioSource();
        hasSwitchedTarget = false; // Initialize the flag
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
        StopRing();
        if (!isPlayingNewClip)
        {
            // Stop the current clip and set the new one
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.Play();
            isPlayingNewClip = true;

            if (!hasSwitchedTarget)
            {
                Invoke("SwitchNextTarget", newClip.length);
            }
        }
    }


    private void SwitchNextTarget()
    {
            audioSource.Stop();
            spotlightController.SwitchToNextTarget();
            hasSwitchedTarget = true; // Update the flag after switching target
        }
    }
