public interface IJob
{
    JobDesignation Owner { get; }
    bool CanPerformWith(ActorAI actor);
    bool IsValid();
    ICommand CreateCommand(ActorAI actor);
}