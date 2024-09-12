using UnityEngine;

public class GhostRetreatingState : GhostBaseState
{
    public float retreatSpeed = 1.5f;
    public float stopRetreatingDistance = 10;

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
            // Calculate direction away from the player
            Vector3 directionAway = (ghost.rb.transform.position - CheckStuffManager.INSTANCE.player.transform.position).normalized;

            // Calculate the new position by moving away from the player
            Vector3 newPosition = ghost.rb.transform.position + directionAway * retreatSpeed * Time.deltaTime;

            // Apply the new position
            ghost.rb.transform.position = newPosition;

            // Calculate the distance between this object and the player
            float distanceToTarget = Vector3.Distance(ghost.rb.transform.position, CheckStuffManager.INSTANCE.player.transform.position);

            // Check if the distance is greater than or equal to the stop retreating distance
            if (distanceToTarget >= stopRetreatingDistance)
            {
                ghost.SwitchState(ghost.IdleState);
            }
        }
    }

    public override void OnCollisionEnter(GhostStateManager ghost, Collision collision)
    {
        // Handle collision if needed
    }

    public override void OnCollisionExit(GhostStateManager ghost, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
            CheckStuffManager.INSTANCE.ghostsTouch--;
        if (CheckStuffManager.INSTANCE.ghostsTouch < 1)
        {
            CheckStuffManager.INSTANCE.ghostIsTouching = false;
        }
    }
}