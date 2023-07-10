using System.Collections.Generic;

public class DestroyJob : Job
{
    private readonly WorldObjectLifecycleManager source;

    public DestroyJob(DestroyDesignation source)
    {
        Owner = source;
        this.source = source.GetComponent<WorldObjectLifecycleManager>();
        DisplayName = $"Destroy {source.name}";
    }

    public override bool CanPerformWith(JobRunner actor)
    {
        return true;
    }

    public override CompositeCommand CreateCommand(JobRunner actor)
    {
        IEnumerable<ICommand> commands = new List<ICommand>()
        {
            new ApproachCommand(actor.NavMeshAgent, new TransformProvider<JobDesignation>(new ConstProvider<JobDesignation>(Owner))),
            new DestroyCommand(source)
        };
        return new CompositeCommand(commands);
    }

    public override ValidationResult IsValid()
    {
        return Owner == null ? ValidationResult.Impossible : ValidationResult.Valid;
    }
}
