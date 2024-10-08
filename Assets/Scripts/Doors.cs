using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [Header("Door Settings")]
    [Tooltip("Name of the key required to open this door (e.g., RedKey1)")]
    public string requiredKeyName;

    [Tooltip("Angle to rotate the door when opened (e.g., 90 for a right angle)")]
    public float openAngle = 90f;

    [Tooltip("Speed at which the door opens")]
    public float openSpeed = 2f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        // Store the initial (closed) rotation of the door
        closedRotation = transform.rotation;
        targetRotation = closedRotation;
    }

    private void Update()
    {
        // Smoothly rotate the door towards the target rotation
        if (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
        }
    }

    /// <summary>
    /// Call this method to attempt opening the door.
    /// This should be linked to your interaction system (e.g., button press when near the door).
    /// </summary>
    public void AttemptOpenDoor()
    {
        if (isOpen)
        {
            Debug.Log("Door is already open.");
            return;
        }

        if (HasRequiredKey())
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("You don't have the required key to open this door.");
            // Optionally, add feedback like a sound or UI message here
        }
    }

    /// <summary>
    /// Checks if the inventory contains the required key.
    /// </summary>
    /// <returns>True if the key is found; otherwise, false.</returns>
    private bool HasRequiredKey()
    {
        if (InventoryVR.Instance == null)
        {
            Debug.LogError("InventoryVR instance not found!");
            return false;
        }

        foreach (Slot slot in InventoryVR.Instance.slots)
        {
            if (slot.ItemInSlot != null)
            {
                string keyName = slot.ItemInSlot.name; // Assumes the key's GameObject name matches requiredKeyName
                if (keyName.Equals(requiredKeyName, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Opens the door by rotating it to the open position.
    /// </summary>
    private void OpenDoor()
    {
        isOpen = true;
        targetRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
        Debug.Log($"Door opened using key: {requiredKeyName}");
        
    }

    /// <summary>
    /// Optional: Closes the door if needed.
    /// </summary>
    public void CloseDoor()
    {
        if (!isOpen)
            return;

        isOpen = false;
        targetRotation = closedRotation;
        Debug.Log("Door closed.");
       
    }

    // Example interaction using trigger and button press
    private void OnTriggerStay(Collider other)
    {
        // Check if the interacting object is the player
        if (other.CompareTag("Player"))
        {
            // Example: Press the primary index trigger to interact (customize as needed)
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                AttemptOpenDoor();
            }
        }
    }
}
