using System.Collections.Generic;
using UnityEngine;

public class DestroyJob : Job
{
    private readonly DestroyDesignation source;
    private GameObjectGameEvent destroyEvent;

    public DestroyJob(DestroyDesignation source, GameObjectGameEvent destroyEvent)
    {
        Owner = source;
        this.source = source;
        this.destroyEvent = destroyEvent;
        DisplayName = $"Destroy {source.name}";
    }

    public override bool CanPerformWith(ActorAI actor)
    {
        return true;
    }

    public override CompositeCommand CreateCommand(ActorAI actor)
    {
        IEnumerable<ICommand> commands = new List<ICommand>()
        {
            new ApproachCommand(actor.NavMeshAgent, Owner.transform),
            new EventCommand<GameObjectGameEvent, GameObject>(destroyEvent, source.gameObject)
        };
        return new CompositeCommand(commands);
    }

    public override bool IsValid()
    {
        return Owner != null;
    }
}
