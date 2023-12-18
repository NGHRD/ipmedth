using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectItem_Test : MonoBehaviour
{
    public GameObject[] rightItems;
    public SpotlightController spotlightController;
    //public GameObject itemToBeActivated;

    private HashSet<GameObject> rightItemsInContainer = new HashSet<GameObject>();
    private bool hasNotSwitchedToNextTarget = false;

    private void OnTriggerEnter(Collider other)
    {
        GameObject item = other.gameObject;

        if (IsRightItem(item) && rightItemsInContainer.Add(item))
        {
            Debug.Log("Correct item placed!");

            // Check if all the right items are in the container
            if (rightItemsInContainer.Count == rightItems.Length && !hasNotSwitchedToNextTarget)
            {
                //AllRightItemsPlaced();
                Debug.Log("alle items zitten erin!");
                hasNotSwitchedToNextTarget = true;
                spotlightController.SwitchToNextTarget();
            }
            else
            {
                Debug.Log("Er Mist nog een Item!");
            }
        }
        else
        {
            Debug.Log("Item doesn't belong in the container.");
        }
    }

    private bool IsRightItem(GameObject item)
    {
        return System.Array.Exists(rightItems, rightItem => rightItem == item);
    }

    /*private void AllRightItemsPlaced()
    {
        Debug.Log("All right items are in the container. Triggering an event...");
        itemToBeActivated.gameObject.SetActive(true);
    }*/
}
