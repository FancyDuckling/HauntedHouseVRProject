using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneMovement : MonoBehaviour
{

    public Transform player;  // The player object or camera rig you want to move
    public Transform leftController;  // Reference to the left controller transform
    public float movementThreshold = 0.1f;  // Threshold to determine what is considered a movement
    public float moveSpeed = 10.0f;  // Speed of movement

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
            Debug.Log("Moving forward");
            // Move the player forward in the direction they are facing
            player.Translate(player.forward * moveSpeed * Time.deltaTime, Space.World);
        }

        // Update the last position
        lastPosition = leftController.position;
    }
}
