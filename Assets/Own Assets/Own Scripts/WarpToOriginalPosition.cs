using Fusion.XR.Shared.Grabbing.NetworkHandColliderBased;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpToOriginalPosition : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        // Save the original position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }
    // Warp the object to its original position and rotation only in the Unity Editor
    public void WarpToOriginal()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
