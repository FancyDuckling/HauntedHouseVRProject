using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneMovement : MonoBehaviour
{

    public Transform player;  // The player object or camera rig you want to move
    public Transform leftController;  // Reference to the left controller transform
    public float movementThreshold = 0.1f;  // Threshold to determine what is considered a movement
    public float moveSpeed = 10.0f;  // Speed of movement

    public float raycastDistance = 1.0f;  // Maximum distance to check for walls
    public LayerMask wallLayerMask;  // LayerMask for detecting walls
    public Transform cameraTransform;  // Reference to the camera (center eye anchor in OVRCameraRig)

    private Vector3 lastPosition;  // To store the last position of the left controller

    void Start()
    {
        // Initialize the last position as the current position of the left controller
        lastPosition = leftController.position;
    }
    void Update()
    {
        // Calculate the movement vector of the left controller
        Vector3 movementVector = leftController.position - lastPosition;

        // Debugging: log the movement vector and the dot product
        Debug.Log("Movement Vector: " + movementVector);
        Debug.Log("Dot Product: " + Vector3.Dot(movementVector, leftController.forward));

        // Check if the movement is forward and beyond the threshold
        if (Vector3.Dot(movementVector, leftController.forward) > movementThreshold)
        {
            // Perform a raycast to detect walls in the camera's forward direction
            if (!IsWallAhead(out float distanceToWall))
            {
                // If no wall detected, move the player forward in the direction of the camera
                player.Translate(cameraTransform.forward * moveSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                Debug.Log("Wall detected at distance: " + distanceToWall);
            }
        }

        // Update the last position
        lastPosition = leftController.position;
    }

    bool IsWallAhead(out float distanceToWall)
    {
        // Cast a ray forward from the camera (headset) position to detect walls
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);  // Use camera's forward
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, raycastDistance, wallLayerMask))
        {
            // Check if the hit object has the tag "WALL"
            if (hit.collider.CompareTag("WALL"))
            {
                distanceToWall = hit.distance;  // Output the distance to the wall
                return true;
            }
        }

        distanceToWall = 0f;  // Set distance to 0 if no wall is detected
        return false;
    }
}
