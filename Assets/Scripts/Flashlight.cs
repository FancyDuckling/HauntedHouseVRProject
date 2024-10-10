using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlightObject;   // The flashlight GameObject
    public Light flashlight;              // The Light component of the flashlight
    //public Transform objectToCheck;       // Assign the object you want to check
    public bool on;
    public Color rayColor = Color.green;  // Color for the visual ray

    public List<Transform> objectsToCheck;    // List of objects to check

    public InventoryVR playerInventory;   // Reference to the player's inventory

    public TextMeshProUGUI turorialText;

    public float raycastMaxDistance = 1.5f;   // Maximum distance to check for keys


    void Start()
    {
        flashlight.enabled = false;  // Initially turn off the light, but not the flashlightObject
    }

    void Update()
    {
        // Handle flashlight toggling
        if (!on && (Input.GetButtonDown("F") || OVRInput.GetDown(OVRInput.Button.Two)))
        {
            flashlight.enabled = true;  // Turn on the light component
            CheckStuffManager.INSTANCE.flashlightOn = true;
            on = true;
            flashlight.color = new Color(0.925f, 0.796f, 0.537f);
        }
        else if (on && (Input.GetButtonDown("F") || OVRInput.GetDown(OVRInput.Button.Two)))
        {
            flashlight.enabled = false;  // Turn off the light component
            CheckStuffManager.INSTANCE.flashlightOn = false;
            on = false;
        }

        // Check if the flashlight is on and check if any object is lit
        if (CheckStuffManager.INSTANCE.flashlightOn)
        {
            foreach (Transform objectToCheck in objectsToCheck)
            {
                CheckObjectWithRaycast(objectToCheck);
            }
        }
    }

    // Method to check individual objects using raycasting
    private void CheckObjectWithRaycast(Transform objectToCheck)
    {
        Vector3 directionToTarget = objectToCheck.position - flashlight.transform.position;
        float distanceToTarget = directionToTarget.magnitude;

        // Check if the object is within the specified raycast range (use a custom smaller distance)
        if (distanceToTarget <= raycastMaxDistance)
        {
            // Check if the object is within the cone of light
            float angleToTarget = Vector3.Angle(flashlight.transform.forward, directionToTarget);
            if (angleToTarget <= flashlight.spotAngle / 2)
            {
                // Raycast to see if there is a direct line of sight between the flashlight and the object
                if (Physics.Raycast(flashlight.transform.position, directionToTarget, out RaycastHit hit, raycastMaxDistance))
                {
                    // Visual ray for debugging
                    Debug.DrawRay(flashlight.transform.position, directionToTarget, rayColor);

                    if (hit.transform == objectToCheck)
                    {
                        Debug.Log("Object " + objectToCheck.name + " is lit by the flashlight!");

                        // Add the object to inventory
                        AddObjectToInventory(hit.transform.gameObject);
                    }
                    else
                    {
                        flashlight.color = new Color(0.925f, 0.796f, 0.537f);  // Reset flashlight color if no object is hit
                    }
                }
                else
                {
                    // Draw the ray even if it doesn't hit the object (for debugging purposes)
                    Debug.DrawRay(flashlight.transform.position, directionToTarget, Color.red);
                }
            }
        }
    }

    // Method to add the object to the player's inventory
    private void AddObjectToInventory(GameObject obj)
    {
        if (playerInventory.AddItemToInventory(obj))
        {
            // Update tutorial text and enable it
            turorialText.text = "Object added to inventory: " + obj.name;
            turorialText.gameObject.SetActive(true);

            Debug.Log("Object added to inventory: " + obj.name);

            // Optionally, hide the text after a delay
            StartCoroutine(HideTutorialTextAfterDelay(3.0f)); // Hide after 3 seconds

        }
        else
        {
            Debug.Log("Failed to add object to inventory, inventory is full.");
        }
    }

    // Coroutine to hide the tutorial text after a delay
    private IEnumerator HideTutorialTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        turorialText.gameObject.SetActive(false);
    }
}