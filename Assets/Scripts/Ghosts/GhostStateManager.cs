using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateManager : MonoBehaviour
{
    GhostBaseState currentState;
    public Vector3 startPosition;
    public GhostIdleState IdleState = new GhostIdleState();
    public GhostAttackingState AttackingState = new GhostAttackingState();
    public GhostRetreatingState RetreatingState = new GhostRetreatingState();
    public bool hasBuued;
    public float attackSpeed = 0.5f;

    public Rigidbody rb;

    // List to hold audio clips
    public List<AudioClip> audioClips = new List<AudioClip>();

    // Reference to AudioSource component
    private AudioSource audioSource;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        //Starting State
        currentState = IdleState;
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        currentState.EnterState(this);
    }

    void Update()
    {
        //Using the states current uppdate
        currentState.UpdateState(this);
    }

    public void SwitchState(GhostBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        currentState.OnCollisionExit(this, collision);
    }

    public void PlayRandomSound()
    {
        if (audioClips.Count > 0) // Check if the list has any sounds
        {
            // Pick a random index from the audioClips list
            int randomIndex = Random.Range(0, audioClips.Count);

            // Set the selected audio clip to the audio source
            audioSource.clip = audioClips[randomIndex];

            // Play the audio clip
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No audio clips assigned!");
        }
    }
}
