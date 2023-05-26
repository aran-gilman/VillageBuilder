using UnityEngine;
using UnityEngine.AI;

public class ApproachCommand : ICommand
{
    public NavMeshAgent Actor { get; private set; }
    public IProvider<Transform> Target { get; private set; }
    public float StopDistanceSqr { get; private set; }

    private Transform cachedTarget;

    public ApproachCommand(NavMeshAgent actor, IProvider<Transform> target, float stopDistance = 1.0f)
    {
        Actor = actor;
        Target = target;
        StopDistanceSqr = stopDistance * stopDistance;
    }

    public void Init()
    {
        cachedTarget = Target.Get();
        if (cachedTarget != null)
        {
            Actor.destination = cachedTarget.position;
        }
    }

    public ICommand.State Execute()
    {
        if (cachedTarget == null)
        {
            Actor.ResetPath();
            return ICommand.State.Invalid;
        }

        Vector3 diff = Actor.destination - Actor.transform.position;
        if (diff.sqrMagnitude < StopDistanceSqr)
        {
            Actor.ResetPath();
            return ICommand.State.Stopped;
        }
        return ICommand.State.Running;
    }

    public void Cancel()
    {
        cachedTarget = null;
        Actor.ResetPath();
    }
}
