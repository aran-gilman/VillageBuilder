public interface IJob
{
    string DisplayName { get; }
    JobDesignation Owner { get; }
    bool CanPerformWith(ActorAI actor);
    bool IsValid();
    ICommand CreateCommand(ActorAI actor);
}