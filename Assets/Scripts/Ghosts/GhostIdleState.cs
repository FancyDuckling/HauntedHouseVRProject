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

    }

    public override void OnCollisionExit(GhostStateManager ghost, Collision collision)
    {

    }
}
