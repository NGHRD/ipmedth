using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerTrigger : MonoBehaviour
{
    public string correctItem = "telefoon"; // Tag for the correct item
    private Transform spawnLocation; // To store the original parent of the item

    private void OnTriggerStay(Collider other)
    {
        // Check if the correct item is in the container
        if (other.name == correctItem)
        {
            Debug.Log("Correct item is in the container.");
            return;
            // Additional code can be added here if you want to do something with the correct item
        }
        else
        {
            Debug.Log("Incorrect item. Returning to original position.");
            other.transform.parent = spawnLocation;
            // Optionally, deactivate the collider for a short time to prevent immediate re-triggering
        }
    }
}

