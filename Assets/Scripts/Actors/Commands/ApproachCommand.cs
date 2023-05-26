using UnityEngine;
using UnityEngine.AI;

public class ApproachCommand : ICommand
{
    public NavMeshAgent Actor { get; private set; }
    public IProvider<Transform> Target { get; private set; }
    public float StopDistanceSqr { get; private set; }

    private VariableProvider<Transform> cachedTarget = new VariableProvider<Transform>();
    public IProvider<Transform> CachedTarget => cachedTarget;

    public ApproachCommand(NavMeshAgent actor, IProvider<Transform> target, float stopDistance = 1.0f)
    {
        Actor = actor;
        Target = target;
        StopDistanceSqr = stopDistance * stopDistance;
    }

    public void Init()
    {
        cachedTarget.Value = Target.Get();
        if (cachedTarget != null)
        {
            Actor.destination = cachedTarget.Value.position;
        }
    }

    public ICommand.State Execute()
    {
        if (cachedTarget.Value == null)
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
        Actor.ResetPath();
    }
}
