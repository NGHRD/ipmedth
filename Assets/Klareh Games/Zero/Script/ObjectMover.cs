using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public GameObject planeObject;  // Assign the plane GameObject in the Unity Editor
    public List<BoxCollider> movePointColliders;  // List of Box Colliders for points
    public float moveSpeed = 5f;  // Speed at which the plane moves
    public float rotationSpeed = 2f;  // Speed at which the plane rotates to face the next point
    public float bankAngle = 45f;  // Angle of banking during turns
    public float rotationDamping = 5f;  // Damping factor for rotation

    private int currentPointIndex = 0;

    void Start()
    {
        if (planeObject == null)
        {
            Debug.LogError("Plane GameObject not assigned!");
        }
        else if (movePointColliders.Count == 0)
        {
            Debug.LogError("No move points assigned!");
        }
    }

    void Update()
    {
        if (planeObject == null || movePointColliders.Count == 0)
        {
            return;  // Exit Update if the plane or move points are not set
        }

        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        // Get the center of the current point's Box Collider
        Vector3 pointCenter = movePointColliders[currentPointIndex].bounds.center;

        // Calculate the direction to the center of the next point
        Vector3 directionToNextPoint = (pointCenter - planeObject.transform.position).normalized;

        // Rotate the plane gradually to face the next point
        Quaternion targetRotation = Quaternion.LookRotation(directionToNextPoint);
        planeObject.transform.rotation = Quaternion.Slerp(planeObject.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Calculate banking rotation
        float angle = Vector3.SignedAngle(planeObject.transform.forward, directionToNextPoint, Vector3.up);
        Quaternion bankRotation = Quaternion.Euler(0, 0, -angle * bankAngle);

        // Apply banking rotation
        Quaternion finalRotation = planeObject.transform.rotation * bankRotation;
        planeObject.transform.rotation = Quaternion.Slerp(planeObject.transform.rotation, finalRotation, rotationDamping * Time.deltaTime);

        // Move the plane forward
        planeObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // Check if the plane is within the range of the current point's Box Collider
        if (movePointColliders[currentPointIndex].bounds.Contains(planeObject.transform.position))
        {
            // Move to the next point in the list
            currentPointIndex = (currentPointIndex + 1) % movePointColliders.Count;
        }
    }
}