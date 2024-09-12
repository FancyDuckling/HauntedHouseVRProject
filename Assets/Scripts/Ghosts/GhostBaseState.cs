using UnityEngine;

public abstract class GhostBaseState
{
    public abstract void EnterState(GhostStateManager ghost);

    public abstract void UpdateState(GhostStateManager ghost);

    public abstract void OnCollisionEnter(GhostStateManager ghost, Collision collision);

    public abstract void OnCollisionExit(GhostStateManager ghost, Collision collision);
}
