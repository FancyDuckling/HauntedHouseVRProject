using UnityEngine;

public class GhostIdleState : GhostBaseState
{
    public override void EnterState(GhostStateManager ghost)
    {
        Debug.Log("Entering idle State");
    }

    public override void UpdateState(GhostStateManager ghost)
    {
        if (CheckStuffManager.INSTANCE.flashlightOn)
        {
            ghost.SwitchState(ghost.AttackingState);
        }
    }

    public override void OnCollisionEnter(GhostStateManager ghost, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            CheckStuffManager.INSTANCE.ghostsTouch++;
            if (CheckStuffManager.INSTANCE.ghostsTouch > 0)
            {
                CheckStuffManager.INSTANCE.ghostIsTouching = true;
            }

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
