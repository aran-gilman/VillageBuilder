public class RepeatCommand : ICommand
{
    public ICommand RepeatedCommand { get; private set; }
    public IProvider<bool> ShouldRepeat { get; private set; }

    public RepeatCommand(ICommand repeatedCommand, IProvider<bool> repeatUntilTrue)
    {
        RepeatedCommand = repeatedCommand;
        ShouldRepeat = repeatUntilTrue;
    }

    // TODO: Validate that the command can be repeated
    public ICommand.State Execute()
    {
        if (RepeatedCommand.Execute() == ICommand.State.Stopped)
        {
            if (!ShouldRepeat.Get())
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
