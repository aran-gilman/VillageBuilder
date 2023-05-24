using System.Collections.Generic;

public class DestroyJob : IJob
{
    public DestroyDesignation Target { get; private set; }

    public DestroyJob(DestroyDesignation target)
    {
        Target = target;
    }

    public bool CanPerformWith(ActorAI actor)
    {
        return true;
    }

    public IEnumerable<ICommand> CreateCommands(ActorAI actor)
    {
        IEnumerable<ICommand> commands = new List<ICommand>()
        {
            new ApproachCommand(actor.NavMeshAgent, Target.transform),
            new ChopCommand(Target.Inventory)
        };
        return commands;
    }

    public bool IsValid()
    {
        return Target != null;
    }
}
