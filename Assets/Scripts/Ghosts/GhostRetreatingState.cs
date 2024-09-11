using UnityEngine;

public class GhostRetreatingState : GhostBaseState
{
    public float backOffSpeed = 0.5f;
    public float stopRetreatingDistance = 100;
    public override void EnterState(GhostStateManager ghost)
    {
        Debug.Log("Entering Retreating State");
    }
    public override void UpdateState(GhostStateManager ghost)
    {
        if (CheckStuffManager.INSTANCE.flashlightOn)
        {
            ghost.SwitchState(ghost.AttackingState);
        }
        else
        {
            // Calculate direction away from the other object
            Vector3 directionAway = (ghost.rb.transform.position - CheckStuffManager.INSTANCE.player.transform.position).normalized;

            // Apply a continuous, small force to float away
            ghost.rb.AddForce(directionAway * backOffSpeed, ForceMode.Force);

            // Calculate the distance between this object and the target
            float distanceToTarget = Vector3.Distance(ghost.rb.transform.position, CheckStuffManager.INSTANCE.player.transform.position);

            // Check if the distance is less than or equal to the trigger distance
            if (distanceToTarget <= stopRetreatingDistance)
            {
                ghost.SwitchState(ghost.IdleState);
            }
        }     
    }
    public override void OnCollisionEnter(GhostStateManager ghost)
    {

    }
}

