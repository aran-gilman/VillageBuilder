public class EventCommand<TGameEvent, TEventArgs> : ICommand where TGameEvent : GameEvent<TEventArgs>
{
    public TGameEvent GameEvent { get; private set; }
    public TEventArgs Args { get; private set; }

    public EventCommand(TGameEvent gameEvent, TEventArgs args)
    {
        GameEvent = gameEvent;
        Args = args;
    }

    public ICommand.State Execute()
    {
        GameEvent.Raise(Args);
        return ICommand.State.Stopped;
    }
}
