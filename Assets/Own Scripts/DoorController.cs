using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
  public enum Orientation
  {
    X,
    Y,
    Z
  };

  public float rotationAmount = -90f; // Adjustable variable for rotation amount
  private bool isOpen = false;
  private bool triggerActive = true;
  private bool timerActive = false;
  public GameObject door;
  public Orientation orientation;

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log(triggerActive);
    if (triggerActive)
    {
      if (!isOpen) // Make sure the object entering the trigger is the player
      {
        OpenDoor();
        isOpen = true;
        triggerActive = false;
        Debug.Log("Door opened!");
      }
      else
      {
        CloseDoor();
        isOpen = false;
        triggerActive = false;
        Debug.Log("Door closed!");
      }
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if(!timerActive)
    {
      timerActive = true;
      Invoke("setTriggerActive", 2);
    }
   
/*    if (isOpen) // Make sure the object exiting the trigger is the player
    {
      isOpen = false;
    }
    else
    {
      isOpen = true;
    }*/
  }

  private void setTriggerActive()
  {
    triggerActive = true;
    timerActive = false;
  }

  private void OpenDoor()
    {
    switch (orientation)
    {
      case Orientation.X:
        door.transform.localRotation *= Quaternion.Euler(rotationAmount, 0f, 0f);
        isOpen = true;
        break;
      case Orientation.Y:
        door.transform.localRotation *= Quaternion.Euler(0f, rotationAmount, 0f);
        isOpen = true;
        break;
      case Orientation.Z:
        door.transform.localRotation *= Quaternion.Euler(0f, 0f, rotationAmount);
        isOpen = true;
        break;
      default:
        Debug.Log("Invalid rotation axis!");
        break;
    }
  }

    private void CloseDoor()
    {
      switch (orientation) {
      case Orientation.X:
        door.transform.localRotation *= Quaternion.Euler(-rotationAmount, 0f, 0f);
        break;
      case Orientation.Y:
        door.transform.localRotation *= Quaternion.Euler(0f, -rotationAmount, 0f);
        break;
      case Orientation.Z:     
        door.transform.localRotation *= Quaternion.Euler(0f, 0f, -rotationAmount);
        break;
      default:
        Debug.Log("Invalid rotation axis!");
        break;
      }
      // Rotate back to the original local rotation
    }
  }
