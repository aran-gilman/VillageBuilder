public abstract class Job
{
    public abstract string DisplayName { get; }
    public abstract JobDesignation Owner { get; }
    public abstract bool CanPerformWith(ActorAI actor);
    public abstract bool IsValid();
    public abstract ICommand CreateCommand(ActorAI actor);
}