using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateManager : MonoBehaviour
{
    GhostBaseState currentState;
    public GhostIdleState IdleState = new GhostIdleState();
    public GhostAttackingState AttackingState = new GhostAttackingState();
    public GhostRetreatingState RetreatingState = new GhostRetreatingState();

    public Rigidbody rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Starting State
        currentState = IdleState;

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
}
