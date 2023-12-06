using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleManager : MonoBehaviour
{
    public static CandleManager Instance;
    public Candle[] candles;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckAllCandlesLit()
    {
        foreach (Candle candle in candles)
        {
            if (!candle.IsLit())
                return;
        }

        Debug.Log("All candles are lit!");
    }
}

