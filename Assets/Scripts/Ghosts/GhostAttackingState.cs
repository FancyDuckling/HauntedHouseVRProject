using UnityEngine;

public class GhostAttackingState : GhostBaseState
{    

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

            // Calculate the new position by moving towards the target
            Vector3 newPosition = ghost.rb.transform.position + directionToTarget * ghost.attackSpeed * Time.deltaTime;

            // Apply the new position
            ghost.rb.transform.position = newPosition;
        }
        if (Vector3.Distance(ghost.transform.position, CheckStuffManager.INSTANCE.player.transform.position) < 3 && !ghost.hasBuued)
        {
            ghost.PlayRandomSound();
            ghost.hasBuued = true;
        }
    }

    public override void OnCollisionEnter(GhostStateManager ghost, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {

            CheckStuffManager.INSTANCE.insanity++;
            if (CheckStuffManager.INSTANCE.ghostsTouch > 0)
            {
                CheckStuffManager.INSTANCE.ghostIsTouching = true;
            }
            ghost.transform.position = ghost.startPosition;
            ghost.hasBuued = false;

        }
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
