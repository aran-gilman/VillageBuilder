public class RepeatCommand : ICommand
{
    public ICommand RepeatedCommand { get; private set; }
    public IProvider<bool> ShouldStop { get; private set; }

    public RepeatCommand(ICommand repeatedCommand, IProvider<bool> repeatUntilTrue)
    {
        RepeatedCommand = repeatedCommand;
        ShouldStop = repeatUntilTrue;
    }

    // TODO: Validate that the command can be repeated
    public ICommand.State Execute()
    {
        if (RepeatedCommand.Execute() == ICommand.State.Stopped)
        {
            if (ShouldStop.Get())
            {
                return ICommand.State.Stopped;
            }
            RepeatedCommand.Init();
        }
        return ICommand.State.Running;
    }

    public void Init()
    {
        RepeatedCommand.Init();
    }

    public void Cancel()
    {
        RepeatedCommand.Cancel();
    }
}
