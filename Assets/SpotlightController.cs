using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpotlightController : MonoBehaviour
{
    public List<Transform> targetObjects; // List of objects the spotlight can look at
    public float rotationSpeed = 5f; // The speed at which the spotlight rotates

    private Light spotlight;
    private Transform currentTarget; // The current target object
    private Quaternion initialRotation; // Initial rotation of the spotlight

    private void Start()
    {
        // Get the Light component attached to this GameObject
        spotlight = GetComponent<Light>();

        if (targetObjects.Count == 0)
        {
            Debug.LogError("No target objects assigned!");
        }

        // Initialize the current target and initial rotation
        if (targetObjects.Count > 0)
        {
            currentTarget = targetObjects[0];
            initialRotation = transform.rotation;
        }
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            // Calculate the direction from the spotlight to the current target object
            Vector3 directionToTarget = currentTarget.position - transform.position;

            // Use Quaternion.Slerp to smoothly rotate the spotlight towards the current target
            Quaternion rotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Function to switch to the next target in the list
    public void SwitchToNextTarget()
    {
        if (targetObjects.Count > 1)
        {
            // Switch to the next target in the list
            int nextIndex = (targetObjects.IndexOf(currentTarget) + 1) % targetObjects.Count;
            currentTarget = targetObjects[nextIndex];

            // Update the rotation coroutine for the new target
            StartCoroutine(RotateToTarget(currentTarget));
        }
    }

    // Coroutine to smoothly rotate to the target over time
    private IEnumerator RotateToTarget(Transform target)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        float rotationSpeedFactor = 1f / rotationSpeed; // Calculate it once

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * rotationSpeedFactor;
            transform.rotation = Quaternion.Slerp(startRotation, initialRotation, elapsedTime);
            yield return null;
        }

        // Ensure the rotation ends exactly at the initial rotation
        transform.rotation = initialRotation;
    }
}
