using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MuteRadio : MonoBehaviour
{
    private Radio radioScript; // Reference to the Radio script

    private void Start()
    {
        // Automatically get the Radio script from the parent GameObject
        radioScript = GetComponentInParent<Radio>();

        if (radioScript == null)
        {
            Debug.LogWarning("Radio script not found on parent GameObject");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Call the ToggleMute function from the Radio script
        radioScript?.ToggleMute();
    }
}
