using UnityEngine;

public class ButtonInteractVR : MonoBehaviour
{
    public float moveDistance = 0.05f;
    public AudioClip buttonClickSound;
    public float transitionSpeed = 1000f;
    public float delayBeforeReset = 0.0f;

    private Vector3 originalPosition;
    private Vector3 maxPressedPosition;
    private AudioSource audioSource;
    private float lerpProgress = 0.0f;
    private bool isControllerInTrigger = false;
    private float resetDelayTimer;
    private bool audioPlaying = false;
    private bool buttonPressed = false;

    public SpotlightController spotlightController;

    private void Awake()
    {
        originalPosition = transform.position;
        maxPressedPosition = originalPosition - new Vector3(0, moveDistance, 0);
        audioSource = GetComponent<AudioSource>();

        // Disable auto-play and looping of the audio
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }

    /*private void FixedUpdate()
    {
        if (!buttonPressed)
        {
            if (isControllerInTrigger)
            {
                lerpProgress += Time.fixedDeltaTime * transitionSpeed;
            }
            else
            {
                if (resetDelayTimer > 0)
                {
                    resetDelayTimer -= Time.fixedDeltaTime;
                }
                else
                {
                    lerpProgress -= Time.fixedDeltaTime * transitionSpeed;
                }
            }

            lerpProgress = Mathf.Clamp01(lerpProgress);
            transform.position = Vector3.Lerp(originalPosition, maxPressedPosition, lerpProgress);
        }
        else { return; }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (!IsValidCollider(other) || buttonPressed) return;

        buttonPressed = true;
        PlayButtonPressSound();
        resetDelayTimer = delayBeforeReset;
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
        if (buttonClickSound != null && audioSource != null && !audioPlaying)
        {
            audioSource.clip = buttonClickSound;
            audioSource.Play();
            audioPlaying = true;
        }
    }

    private void Update()
    {
        if (audioPlaying && !audioSource.isPlaying)
        {
            SwitchToNextTarget();
        }
    }

    private void SwitchToNextTarget()
    {
        if (spotlightController != null)
        {
            spotlightController.SwitchToNextTarget();
        }

        audioPlaying = false;
    }

    private bool IsValidCollider(Collider collider)
    {
        return true; // Placeholder, replace with actual checking logic
    }
}
