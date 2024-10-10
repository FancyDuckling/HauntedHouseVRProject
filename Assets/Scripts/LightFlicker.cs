using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    // Reference to the Light component
    private Light pointLight;

    // Minimum and maximum intensity for the light flicker
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;

    // Speed at which the light flickers
    public float flickerSpeed = 0.1f;

    // Variable to hold random intensity
    private float targetIntensity;

    void Start()
    {
        // Get the Light component attached to this GameObject
        pointLight = GetComponent<Light>();
    }

    void Update()
    {
        // Randomly adjust the light's intensity over time
        targetIntensity = Random.Range(minIntensity, maxIntensity);

        // Smoothly transition the light intensity to the target value
        pointLight.intensity = Mathf.Lerp(pointLight.intensity, targetIntensity, flickerSpeed * Time.deltaTime);
    }
}
