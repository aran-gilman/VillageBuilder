using System.Collections.Generic;

public class DestroyJob : Job
{
    public DestroyDesignation Target { get; private set; }

    public DestroyJob(DestroyDesignation target)
    {
        Target = target;
    }

    public override bool CanPerformWith(ActorAI actor)
    {
        return true;
    }

    public override IEnumerable<ICommand> CreateCommands(ActorAI actor)
    {
        IEnumerable<ICommand> commands = new List<ICommand>()
        {
            new ApproachCommand(actor.NavMeshAgent, Target.transform),
            new ChopCommand(Target.Inventory)
        };
        return commands;
    }

    public override bool IsValid()
    {
        return Target != null;
    }
}
