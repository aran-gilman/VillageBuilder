public abstract class Job
{
    public enum JobStatus
    {
        Unassigned,
        Assigned
    }
    public JobStatus Status { get; set; }

    public abstract string DisplayName { get; }
    public abstract JobDesignation Owner { get; }
    public abstract bool CanPerformWith(ActorAI actor);
    public abstract bool IsValid();
    public abstract ICommand CreateCommand(ActorAI actor);
}