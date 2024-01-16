using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectItems : MonoBehaviour
{
    //public string correctItem = "telefoon";
    public  string Radio = "Radio";
    public string Juwelen = "Juwelen";// Tag for the correct item
    private Transform spawnLocation; // To store the original parent of the item

    private void OnTriggerStay(Collider other)
    {
        // Check if the correct item is in the container
        if (other.name == Radio && other.name == Juwelen)
        {
            Debug.Log("Correct item is in the container.");
            return;
            // Additional code can be added here if you want to do something with the correct item
        }
        else if(other.name == Radio || other.name == Juwelen)
        {
            Debug.Log("Item Missing, Waiting for second item");
            return;
            // Additional code to put one of the items on set spot
        }
        else
        {
            Debug.Log("Incorrect item. Returning to original position.");
            other.transform.parent = spawnLocation;
            // Optionally, deactivate the collider for a short time to prevent immediate re-triggering
        }
    }
}