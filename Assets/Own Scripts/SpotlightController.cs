using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class SpotlightController : MonoBehaviour
{
    public List<TransformEventPair> targetObjects; // List of objects the spotlight can look at with associated events

    private Transform currentTarget; // The current target object

    [System.Serializable]
    public class TransformEventPair
    {
        public Transform target;
        public UnityEvent currentEvent; // UnityEvent for the associated function
    }

    private void Start()
    {

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

            // Check if the current target has an associated event and invoke it
            UnityEvent currentEvent = targetObjects.Find(item => item.target == currentTarget)?.currentEvent;
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
