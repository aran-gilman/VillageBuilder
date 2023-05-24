using System.Collections.Generic;

public abstract class Job
{
    public abstract bool CanPerformWith(ActorAI actor);
    public abstract bool IsValid();
    public abstract IEnumerable<ICommand> CreateCommands(ActorAI actor);
}