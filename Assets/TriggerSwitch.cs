using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitch : MonoBehaviour
{
    private bool hasSwitchedTargets = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasSwitchedTargets)
        {
            SpotlightController spotlightController = FindObjectOfType<SpotlightController>();

            if (spotlightController != null)
            {
                spotlightController.SwitchToNextTarget();
                hasSwitchedTargets = true;
            }
        }
    }
}
