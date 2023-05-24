using System.Collections.Generic;

public interface IJob
{
    bool CanPerformWith(ActorAI actor);
    bool IsValid();
    IEnumerable<ICommand> CreateCommands(ActorAI actor);
}