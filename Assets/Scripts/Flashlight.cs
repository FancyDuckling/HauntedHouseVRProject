using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlightObject;   // The flashlight GameObject
    public Light flashlight;              // The Light component of the flashlight
    public Transform objectToCheck;       // Assign the object you want to check
    public bool on;
    public bool off;

    void Start()
    {
        off = true;
        flashlightObject.SetActive(false);  // Disable the flashlight at start
    }

    void Update()
    {
        // Handle flashlight toggling
        if (off && (Input.GetButtonDown("F") || OVRInput.GetDown(OVRInput.Button.Two)))
        {
            flashlightObject.SetActive(true);  // Turn on the flashlight
            CheckStuffManager.INSTANCE.flashlightOn = true;
            off = false;
            on = true;
        }
        else if (on && (Input.GetButtonDown("F") || OVRInput.GetDown(OVRInput.Button.Two)))
        {
            flashlightObject.SetActive(false);  // Turn off the flashlight
            CheckStuffManager.INSTANCE.flashlightOn = false;
            off = true;
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
                        if (hit.transform == objectToCheck)
                        {
                            Debug.Log("Object is lit by the flashlight!");
                        }
                    }
                }
            }
        }
    }
}