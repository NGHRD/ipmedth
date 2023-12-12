using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidEffect : MonoBehaviour
{
    public Transform fluidTransform;
    public float maxTiltAngle = 45.0f;
    public float maxFluidHeight; // Maximum height of the fluid
    private Vector3 originalScale; // Original scale of the fluid

    void Start()
    {
        // Store the original scale of the fluid
        originalScale = fluidTransform.localScale;
        // Assume the original fluid height represents 100%
        maxFluidHeight = fluidTransform.localScale.y;
    }

    void Update()
    {
        // Get the tilt of the bottle
        Vector3 bottleTilt = transform.eulerAngles;

        // Calculate the fluid's tilt angle
        float tiltX = Mathf.Clamp(bottleTilt.x, -maxTiltAngle, maxTiltAngle);
        float tiltZ = Mathf.Clamp(bottleTilt.z, -maxTiltAngle, maxTiltAngle);

        // Apply the tilt to the fluid
        fluidTransform.localEulerAngles = new Vector3(-tiltX, 0, -tiltZ);
    }

    // Call this method to set the fluid level (0 to 100%)
    public void SetFluidLevel(float percentage)
    {
        percentage = Mathf.Clamp(percentage, 0f, 100f);
        float newHeight = maxFluidHeight * (percentage / 100f);
        fluidTransform.localScale = new Vector3(originalScale.x, newHeight, originalScale.z);
    }
}
