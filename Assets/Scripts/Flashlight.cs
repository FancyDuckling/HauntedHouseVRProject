using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlightObject;   // The flashlight GameObject
    public Light flashlight;              // The Light component of the flashlight
    public Transform objectToCheck;       // Assign the object you want to check
    public bool on;
    public Color rayColor = Color.green;  // Color for the visual ray

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

        // Check if the flashlight is on and check if the object is lit
        if (CheckStuffManager.INSTANCE.flashlightOn)
        {
            Vector3 directionToTarget = objectToCheck.position - flashlight.transform.position;
            float distanceToTarget = directionToTarget.magnitude;

            // Check if the object is within the flashlight's range and cone angle
            if (distanceToTarget <= flashlight.range)
            {
                // Check if the object is within the cone of light
                float angleToTarget = Vector3.Angle(flashlight.transform.forward, directionToTarget);
                if (angleToTarget <= flashlight.spotAngle / 2)
                {
                    // Raycast to see if there is a direct line of sight between the flashlight and the object
                    if (Physics.Raycast(flashlight.transform.position, directionToTarget, out RaycastHit hit, flashlight.range))
                    {
                        // Visual ray for debugging
                        Debug.DrawRay(flashlight.transform.position, directionToTarget, rayColor);

                        if (hit.transform == objectToCheck)
                        {
                            flashlight.color = Color.red;
                            Debug.Log("Object is lit by the flashlight!");
                        }
                        else
                            flashlight.color = new Color(0.925f, 0.796f, 0.537f);
                    }
                    else
                    {
                        // Draw the ray even if it doesn't hit the object (for debugging purposes)
                        Debug.DrawRay(flashlight.transform.position, directionToTarget, Color.red);
                        
                    }
                }
            }
        }
    }
}