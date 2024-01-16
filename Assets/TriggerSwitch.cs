using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitch : MonoBehaviour
{
    private bool hasSwitchedTargets = false;
    public int preferedTargetIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasSwitchedTargets)
        {
            SpotlightController spotlightController = FindObjectOfType<SpotlightController>();

             if (spotlightController.targetIndex == preferedTargetIndex && spotlightController != null && !hasSwitchedTargets)
            {
                spotlightController.SwitchToNextTarget();
                hasSwitchedTargets = true;
            }
        }
    }
}
