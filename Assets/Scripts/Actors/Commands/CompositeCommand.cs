using System.Collections.Generic;

public class CompositeCommand : ICommand
{
    private CommandRunner commandRunner = new CommandRunner();

    public IEnumerable<ICommand> Commands { get; private set; }

    public CompositeCommand(IEnumerable<ICommand> commands)
    {
        Commands = commands;
    }

    public ICommand.State Execute()
    {
        commandRunner.Run();
        return commandRunner.IsIdle ? ICommand.State.Stopped : ICommand.State.Running;
    }

    public void Init()
    {
        commandRunner.AddCommands(Commands);
    }
}
