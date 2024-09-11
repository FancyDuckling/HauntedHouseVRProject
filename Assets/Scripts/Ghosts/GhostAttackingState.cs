using UnityEngine;

public class GhostAttackingState : GhostBaseState
{
    public float attackSpeed = 2f;
    public override void EnterState(GhostStateManager ghost)
    {
        Debug.Log("Entering Attacking State");
    }

    public override void UpdateState(GhostStateManager ghost)
    {
        if (!CheckStuffManager.INSTANCE.flashlightOn)
        {
            ghost.SwitchState(ghost.RetreatingState);
        }
        else
        {
            // Calculate direction towards the target
            Vector3 directionToTarget = (CheckStuffManager.INSTANCE.player.transform.position - ghost.rb.transform.position).normalized;

            // Apply a continuous force towards the target
            ghost.rb.AddForce(directionToTarget * attackSpeed, ForceMode.Force);
        }
    }

    public override void OnCollisionEnter(GhostStateManager ghost)
    {

    }
}
