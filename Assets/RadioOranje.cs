using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioOranje : MonoBehaviour
{
    public GameObject[] rightItems;
    public GameObject itemToBeActivated;
    private float movementSpeed = 5f;

    private HashSet<GameObject> rightItemsInContainer = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        GameObject item = other.gameObject;
        

        if (IsRightItem(item) && rightItemsInContainer.Add(item))
        {
            Debug.Log("Correct item placed!");

            // Check if all the right items are in the container
            if (rightItemsInContainer.Count == rightItems.Length)
            {
                AllRightItemsPlaced();
                //get the Input from Horizontal axis
                float horizontalInput = Input.GetAxis("Horizontal");
                //get the Input from Vertical axis
                float verticalInput = Input.GetAxis("Vertical");

                //update the position
                transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);

                //output to log the position change
                Debug.Log("alle items zitten erin!");
            }
            else
            {
                Debug.Log("Er Mist nog een Item!");
            }
        }
        else
        {
            Debug.Log("Item doesn't belong in the container.");
            item.GetComponent<WarpToOriginalPosition>().WarpToOriginal();
        }
    }

    private bool IsRightItem(GameObject item)
    {
        return System.Array.Exists(rightItems, rightItem => rightItem == item);
    }

    private void AllRightItemsPlaced()
    {
        Debug.Log("All right items are in the container. Triggering an event...");
        itemToBeActivated.gameObject.SetActive(true);
    }
}