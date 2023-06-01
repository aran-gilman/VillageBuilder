public class ConditionalCommand : ICommand
{
    public IProvider<bool> ShouldExecute { get; private set; }
    public ICommand Command { get; private set; }

    private VariableProvider<bool> didExecute = new VariableProvider<bool>();
    public IProvider<bool> DidExecute => didExecute;

    public ConditionalCommand(IProvider<bool> shouldExecute, ICommand command)
    {
        ShouldExecute = shouldExecute;
        Command = command;
    }

    public ICommand.State Execute()
    {
        if (!didExecute.Value)
        {
            return ICommand.State.Stopped;
        }
        return Command.Execute();
    }

    public void Init()
    {
        didExecute.Value = ShouldExecute.Get();
        if (didExecute.Value)
        {
            Command.Init();
        }
    }

    public void Cancel()
    {
        if (didExecute.Value)
        {
            Command.Cancel();
        }
    }
}
