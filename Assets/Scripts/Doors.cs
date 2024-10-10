using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public TextMeshProUGUI tutorialText;

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

            tutorialText.text = "You don't have the required key to open this door.";
            tutorialText.gameObject.SetActive(true);

           
            StartCoroutine(HideTutorialTextAfterDelay(3.0f));

            Debug.Log("You don't have the required key to open this door.");
            
        }
    }

   
    // Checks if the inventory contains the required key.
    
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
                string keyName = slot.ItemInSlot.name; 
                if (keyName.Equals(requiredKeyName, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }

        return false;
    }

    
    // Opens the door by rotating it to the open position.
    
    private void OpenDoor()
    {
        isOpen = true;
        targetRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);

        // Update the tutorialText to inform the player the door is opened
        tutorialText.text = $"Door opened using key: {requiredKeyName}";
        tutorialText.gameObject.SetActive(true);

        // Optionally hide the tutorialText after a short delay
        StartCoroutine(HideTutorialTextAfterDelay(3.0f));

        Debug.Log($"Door opened using key: {requiredKeyName}");
        
    }

   
    // Optional: Closes the door if needed.
 
    public void CloseDoor()
    {
        if (!isOpen)
            return;

        isOpen = false;
        targetRotation = closedRotation;
        Debug.Log("Door closed.");
       
    }

    //interaction using trigger and button press
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

    // Coroutine to hide the tutorialText after a delay
    private IEnumerator HideTutorialTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        tutorialText.gameObject.SetActive(false);
    }
}
