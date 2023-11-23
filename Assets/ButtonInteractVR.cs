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

    void Start()
    {
        originalPosition = transform.position;
        maxPressedPosition = originalPosition - new Vector3(0, moveDistance, 0);
        audioSource = GetComponent<AudioSource>();
        resetDelayTimer = delayBeforeReset;
    }

    private void Update()
    {
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

        if (!isControllerInTrigger)
        {
            isControllerInTrigger = true;
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
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
        // Add haptic feedback here if your VR SDK supports it
    }

    private bool IsValidCollider(Collider collider)
    {
        // Implement your logic to determine if the collider is valid for pressing the button
        return true; // Placeholder, replace with actual checking logic
    }
}
