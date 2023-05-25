using System.Collections.Generic;

public class CompositeCommand : ICommand
{
    public CommandRunner CommandRunner { get; } = new CommandRunner();

    public IEnumerable<ICommand> Commands { get; private set; }

    public CompositeCommand(IEnumerable<ICommand> commands)
    {
        Commands = commands;
    }

    public ICommand.State Execute()
    {
        CommandRunner.Run();
        return CommandRunner.IsIdle ? ICommand.State.Stopped : ICommand.State.Running;
    }

    public void Init()
    {
        CommandRunner.AddCommands(Commands);
    }
}