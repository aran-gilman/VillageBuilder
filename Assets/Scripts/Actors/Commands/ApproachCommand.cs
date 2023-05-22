using UnityEngine;
using UnityEngine.AI;

public class ApproachCommand : ICommand
{
    public NavMeshAgent Actor { get; private set; }
    public Transform Target { get; private set; }
    public float StopDistanceSqr { get; private set; }

    public ApproachCommand(NavMeshAgent actor, Transform target, float stopDistance = 1.0f)
    {
        Actor = actor;
        Target = target;
        StopDistanceSqr = stopDistance * stopDistance;
    }

    public void Init()
    {
        Actor.destination = Target.position;
    }

    public ICommand.State Execute()
    {
        Vector3 diff = Actor.destination - Actor.transform.position;
        if (diff.sqrMagnitude < StopDistanceSqr)
        {
            Actor.ResetPath();
            return ICommand.State.Stopped;
        }
        return ICommand.State.Running;
    }
}
