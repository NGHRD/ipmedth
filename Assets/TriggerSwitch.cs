using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitch : MonoBehaviour
{
    private bool hasSwitchedTargets = false;

    public SpotlightController spotlightController;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasSwitchedTargets && spotlightController != null)
        {
            spotlightController.SwitchToNextTarget();
            hasSwitchedTargets = true;
        }
    }

    public void ResetSwitchedTargetsFlag()
    {
        hasSwitchedTargets = false;
    }
}
