using UnityEngine;

public class CheckStuffManager : MonoBehaviour
{
    public static CheckStuffManager INSTANCE;

    public bool flashlightOn;
    public int ghostsTouch;
    public bool ghostIsTouching;
    public int insanity;
    public GameObject player;
    public InsanityCanvas insanityCanvas;

    private void Awake()
    {
        if (INSTANCE != null)
        {
            Destroy(gameObject);
        }
        else
        {
            INSTANCE = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (insanityCanvas != null)
        {
            // Control visibility of images based on the insanity level
            if (insanity == 1)
            {
                insanityCanvas.SetImageVisibility(0, true);  // Show the first image
            }
            else if (insanity == 2)
            {
                insanityCanvas.SetImageVisibility(1, true);  // Show the second image
            }
            else if (insanity == 3)
            {
                insanityCanvas.SetImageVisibility(2, true);  // Show the third image
            }
            else if (insanity == 4)
            {
                insanityCanvas.SetImageVisibility(3, true);  // Show the fourth image
            }
            else if (insanity == 5)
            {
                // Custom logic for insanity == 5 if needed
            }
        }
    }
}