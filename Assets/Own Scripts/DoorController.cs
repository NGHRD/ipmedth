using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float rotationAmount = -90f; // Adjustable variable for rotation amount
    private bool isOpen = false;
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen) // Make sure the object entering the trigger is the player
        {
            OpenDoor();
            Debug.Log("Door opened!");
        }
        else
        {
            CloseDoor();
            Debug.Log("Door closed!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isOpen) // Make sure the object exiting the trigger is the player
        {
            isOpen = false;
        }
    }

    private void OpenDoor()
    {
        door.transform.localRotation *= Quaternion.Euler(0f, 0f, rotationAmount); // Rotate locally around the Y-axis
        isOpen = true;
    }

    private void CloseDoor()
    {
        door.transform.localRotation *= Quaternion.Euler(0f, 0f, -rotationAmount); // Rotate back to the original local rotation
    }
}
