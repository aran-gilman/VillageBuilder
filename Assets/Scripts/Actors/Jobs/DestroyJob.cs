using System.Collections.Generic;

public class DestroyJob : IJob
{
    private readonly DestroyDesignation source;

    public JobDesignation Owner => source;

    public DestroyJob(DestroyDesignation source)
    {
        this.source = source;
    }

    public bool CanPerformWith(ActorAI actor)
    {
        return true;
    }

    public ICommand CreateCommand(ActorAI actor)
    {
        IEnumerable<ICommand> commands = new List<ICommand>()
        {
            new ApproachCommand(actor.NavMeshAgent, Owner.transform),
            new ChopCommand(source.Inventory)
        };
        return new CompositeCommand(commands);
    }

    public bool IsValid()
    {
        return Owner != null;
    }
}
