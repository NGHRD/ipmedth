using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleManager : MonoBehaviour
{
    public static CandleManager Instance;
    public Candle[] candles;

    public SpotlightController spotlightController;
    private bool hasSwitchedToNextTarget; // Flag to track if the switch has occurred

    private void Awake()
    {
        Instance = this;
        hasSwitchedToNextTarget = false; // Initialize the flag
    }

    public void CheckAllCandlesLit()
    {
        foreach (Candle candle in candles)
        {
            if (!candle.IsLit())
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
