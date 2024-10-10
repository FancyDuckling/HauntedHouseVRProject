using UnityEngine;
using UnityEngine.UI;

public class InsanityCanvas : MonoBehaviour
{
    public RectTransform[] images; // Array for the 4 RectTransforms
    public float radius = 150f;    // Radius of the circular path
    public float speed = 50f;      // Speed of the rotation
    private float angleStep;       // Angle between each image

    void Start()
    {
        // Calculate the angle between each image in radians (360 degrees divided by 4 images)
        angleStep = 360f / images.Length;

        // Make all images invisible at the start by disabling their GameObjects
        foreach (var image in images)
        {
            image.gameObject.SetActive(false); // Start by disabling the GameObject
        }
    }

    void Update()
    {
        // For each image, calculate its position in a circular path
        for (int i = 0; i < images.Length; i++)
        {
            // Calculate current angle for this image based on time and speed
            float angle = (Time.time * speed + i * angleStep) * Mathf.Deg2Rad;

            // Set the new position of the image relative to the center
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            images[i].anchoredPosition = new Vector2(x, y);
        }
    }

    // Method to enable or disable visibility of the images based on the insanity level
    public void SetImageVisibility(int index, bool isVisible)
    {
        if (index >= 0 && index < images.Length)
        {
            // Enable or disable the entire GameObject
            images[index].gameObject.SetActive(isVisible);
        }
    }
}