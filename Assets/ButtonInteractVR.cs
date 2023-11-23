using UnityEngine;

public class ButtonInteractVR : MonoBehaviour
{
    public float moveDistance = 0.05f; // Maximum distance to move the button down
    public AudioClip buttonClickSound; // Assign in inspector
    public float transitionSpeed = 1000f; // Speed of the transition
    public float delayBeforeReset = 0.0f; // Delay before the button starts moving up after being released

    private Vector3 originalPosition;
    private Vector3 maxPressedPosition;
    private AudioSource audioSource;
    private float lerpProgress = 0.0f; // Lerp progress
    private bool isControllerInTrigger = false;
    private float resetDelayTimer;
    private bool audioPlaying = false; // Flag to track if audio is playing
    private bool buttonPressed = false; // Flag to track if the button is currently pressed

    // Reference to the SpotlightController script
    public SpotlightController spotlightController;

    void Start()
    {
        originalPosition = transform.position;
        maxPressedPosition = originalPosition - new Vector3(0, moveDistance, 0);
        audioSource = GetComponent<AudioSource>();
        resetDelayTimer = delayBeforeReset;

        // Disable auto-play and looping of the audio
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }

    private void Update()
    {
        if (buttonPressed)
        {
            // If the button is pressed, don't allow further interaction until it has reset
            return;
        }

        if (isControllerInTrigger)
        {
            lerpProgress += Time.deltaTime * transitionSpeed;
        }
        else
        {
            if (resetDelayTimer > 0)
            {
                resetDelayTimer -= Time.deltaTime;
            }
            else
            {
                lerpProgress -= Time.deltaTime * transitionSpeed;
            }
        }

        lerpProgress = Mathf.Clamp(lerpProgress, 0.0f, 1.0f);
        transform.position = Vector3.Lerp(originalPosition, maxPressedPosition, lerpProgress);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsValidCollider(other)) return;

        if (!buttonPressed)
        {
            buttonPressed = true;
            PlayButtonPressSound();
            resetDelayTimer = delayBeforeReset; // Reset the timer
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidCollider(other))
        {
            isControllerInTrigger = false;
        }
    }

    private void PlayButtonPressSound()
    {
        // Only play the sound if it's not already playing
        if (buttonClickSound != null && audioSource != null && !audioPlaying)
        {
            audioSource.clip = buttonClickSound;
            audioSource.Play();
            audioPlaying = true;

            // Delay switching to the next target until the audio clip duration
            Invoke("SwitchToNextTarget", buttonClickSound.length);
        }
        // Add haptic feedback here if your VR SDK supports it
    }

    private void SwitchToNextTarget()
    {
        // Call the function to switch to the next target in the SpotlightController
        if (spotlightController != null)
        {
            spotlightController.SwitchToNextTarget();
        }

        // Reset the button state after switching to the next target
        buttonPressed = false;
    }

    private bool IsValidCollider(Collider collider)
    {
        // Implement your logic to determine if the collider is valid for pressing the button
        return true; // Placeholder, replace with actual checking logic
    }
}
