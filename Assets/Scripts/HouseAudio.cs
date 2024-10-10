using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAudio : MonoBehaviour
{
    // List of audio clips to cycle through
    public List<AudioClip> audioClips;

    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Index to track the current audio clip
    private int currentClipIndex = 0;

    // Delay time between clips (in seconds)
    public float delayBetweenClips = 5.0f; // You can set this from the Inspector

    void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // Start the coroutine to cycle through audio clips
        if (audioClips.Count > 0)
        {
            StartCoroutine(CycleAudioWithDelay());
        }
    }

    // Coroutine to handle playing clips with a delay between them
    IEnumerator CycleAudioWithDelay()
    {
        while (true)
        {
            // Play the current audio clip
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();

            // Wait for the current clip to finish playing
            yield return new WaitForSeconds(audioSource.clip.length);

            // Wait for the additional delay between clips
            yield return new WaitForSeconds(delayBetweenClips);

            // Move to the next clip, wrapping around if necessary
            currentClipIndex = (currentClipIndex + 1) % audioClips.Count;
        }
    }
}
