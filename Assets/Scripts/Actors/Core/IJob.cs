public interface IJob
{
    bool CanPerformWith(ActorAI actor);
    bool IsValid();
    ICommand CreateCommand(ActorAI actor);
}