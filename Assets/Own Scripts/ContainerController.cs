using UnityEngine;
using System.Collections.Generic;

public class ContainerController : MonoBehaviour
{
    public GameObject[] rightItems;
    public List<GameObject> lightBulbs; // List of light bulb GameObjects
    public Material greenMaterial; // The green material to assign
    public GameObject itemToBeActivated;

    private HashSet<GameObject> rightItemsInContainer = new HashSet<GameObject>();
    private int greenLightBulbsCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        GameObject item = other.gameObject;

        if (IsRightItem(item) && rightItemsInContainer.Add(item))
        {
            Debug.Log("Correct item placed!");

            // Check if all the right items are in the container
            if (rightItemsInContainer.Count == rightItems.Length)
            {
                IncrementGreenLightBulbs();
                AllRightItemsPlaced();
            }
            else
            {
                IncrementGreenLightBulbs();
                Debug.Log("groen bulb aan!");
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

    private void IncrementGreenLightBulbs()
    {
        // Check if there are more light bulbs to turn green
        if (greenLightBulbsCount < lightBulbs.Count)
        {
            GameObject lightBulb = lightBulbs[greenLightBulbsCount];
            LightBulb bulbController = lightBulb.GetComponent<LightBulb>();
            if (bulbController != null)
            {
                bulbController.AddGreenMaterial(greenMaterial);
            }

            greenLightBulbsCount++;
        }
    }
}
