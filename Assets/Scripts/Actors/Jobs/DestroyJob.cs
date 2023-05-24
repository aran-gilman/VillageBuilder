using System.Collections.Generic;

public class DestroyJob : Job
{
    private readonly DestroyDesignation source;

    public override JobDesignation Owner => source;

    private string displayName;
    public override string DisplayName => displayName;

    public DestroyJob(DestroyDesignation source)
    {
        this.source = source;
        displayName = $"Destroy {source.name}";
    }

    public override bool CanPerformWith(ActorAI actor)
    {
        return true;
    }

    public override ICommand CreateCommand(ActorAI actor)
    {
        IEnumerable<ICommand> commands = new List<ICommand>()
        {
            new ApproachCommand(actor.NavMeshAgent, Owner.transform),
            new ChopCommand(source.Inventory)
        };
        return new CompositeCommand(commands);
    }

    public override bool IsValid()
    {
        return Owner != null;
    }
}
