using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class SpotlightController : MonoBehaviour
{
    public List<TransformEventPair> targetObjects; // List of objects the spotlight can look at with associated events
    public float rotationSpeed = 5f; // The speed at which the spotlight rotates

    private Light spotlight;
    private Transform currentTarget; // The current target object
    private PhoneScript currentPhoneScript; // Reference to the current PhoneScript

    [System.Serializable]
    public class TransformEventPair
    {
        public Transform target;
        public UnityEvent lookedAtEvent; // UnityEvent for the associated function
    }

    private void Start()
    {
        // Get the Light component attached to this GameObject
        spotlight = GetComponent<Light>();

        if (targetObjects.Count == 0)
        {
            Debug.LogError("No target objects assigned!");
        }

        // Initialize the current target
        if (targetObjects.Count > 0)
        {
            currentTarget = targetObjects[0].target;
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

            // Check if the current target has an associated event and invoke it
            UnityEvent currentEvent = targetObjects.Find(item => item.target == currentTarget)?.lookedAtEvent;
            if (currentEvent != null)
            {
                currentEvent.Invoke();
            }
        }
    }

    // Function to switch to the next target in the list
    public void SwitchToNextTarget()
    {
        if (targetObjects.Count > 1)
        {

            // Switch to the next target in the list
            int nextIndex = (targetObjects.FindIndex(item => item.target == currentTarget) + 1) % targetObjects.Count;
            currentTarget = targetObjects[nextIndex].target;

        }
    }
}
