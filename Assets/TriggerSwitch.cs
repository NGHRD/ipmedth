using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitch : MonoBehaviour
{
    private bool hasSwitchedTargets = false;

    public SpotlightController spotlightController;
    public int preferedTargetIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (spotlightController.targetIndex == preferedTargetIndex && spotlightController != null && !hasSwitchedTargets)
        {
            Debug.Log("Standbeeld is aangeraakt");
            spotlightController.SwitchToNextTarget();
            hasSwitchedTargets = true;
            
        }
    }

    public void ResetSwitchedTargetsFlag()
    {
        hasSwitchedTargets = false;
        Debug.Log("Wow Ja");
    }

    public void SetPreferedTargetIndex(int index)
    {
        preferedTargetIndex = index;
        Debug.Log("Werkt deze index?");
    }
}