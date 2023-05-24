public abstract class Job
{
    public enum JobStatus
    {
        Unassigned,
        Assigned
    }
    public JobStatus Status { get; set; }
    public ActorAI Assignee { get; set; }

    public string DisplayName { get; protected set; }
    public JobDesignation Owner { get; protected set; }

    public abstract bool CanPerformWith(ActorAI actor);
    public abstract bool IsValid();
    public abstract ICommand CreateCommand(ActorAI actor);
}