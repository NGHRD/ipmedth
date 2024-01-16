using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light flickeringLight;
    public float minIntensity = 1f;
    public float maxIntensity = 2f;
    public float flickerSpeed = 1f;

    void Start()
    {
        if (flickeringLight == null)
        {
            flickeringLight = GetComponent<Light>();
        }

        // Start the flickering coroutine
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            // Calculate a random intensity value between min and max
            float randomIntensity = Random.Range(minIntensity, maxIntensity);

            // Set the light intensity
            flickeringLight.intensity = randomIntensity;

            // Wait for a short duration before flickering again
            yield return new WaitForSeconds(1 / flickerSpeed);
        }
    }
}

