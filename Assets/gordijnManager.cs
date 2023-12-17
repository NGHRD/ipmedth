using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gordijnManager : MonoBehaviour
{
    public static gordijnManager Instance;
    public Gordijn_Dicht[] gordijnen;

    public SpotlightController spotlightController;
    private bool hasSwitchedToNextTarget;

    private void Awake()
    {
        Instance = this;
        hasSwitchedToNextTarget = false; // Initialize the flag
    }

    public void CheckAllGordijnDicht()
    {
        foreach (Gordijn_Dicht gordijn in gordijnen)
        {
            if (!gordijn.IsDicht())
                return;
        }

        // Call SwitchToNextTarget only if it hasn't been called before
        if (!hasSwitchedToNextTarget)
        {
            spotlightController.SwitchToNextTarget();
            hasSwitchedToNextTarget = true; // Update the flag
        }
    }
}
