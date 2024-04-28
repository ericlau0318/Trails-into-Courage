using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderFollowAnimation : MonoBehaviour
{
    public BoxCollider colliderToUpdate; // Reference to the Box Collider component
    public Transform joint1; // Assign the first joint in the inspector
    public Transform joint2;
    // Update is called once per frame
    public float colliderCenterYOffset = 0f; // Adjust this value as needed to lower the collider
    public Vector3 colliderSize = new(0.02f, 0.015f, 0.02f); // Set your static collider size here
    void LateUpdate()
    {
        // Calculate the center position between the two joints
        Vector3 midPoint = (joint1.position + joint2.position) / 2;
        // Adjust the Y component of the midpoint by subtracting an offset value
        midPoint.y -= colliderCenterYOffset;

        // Set the collider's center to the adjusted midpoint, in the local space of the collider's GameObject
        colliderToUpdate.center = transform.InverseTransformPoint(midPoint);

        // Set the collider's size to the predefined static size
        colliderToUpdate.size = colliderSize;
    }
}
