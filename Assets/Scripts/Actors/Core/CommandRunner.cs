using System;
using System.Collections.Generic;

public class CommandRunner
{
    public event EventHandler OnBecomeIdle;

    public bool IsIdle => queue.Count == 0;

    private List<ICommand> queue = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        queue.Add(command);
        if (queue.Count == 1)
        {
            command.Init();
        }
    }

    public void AddCommands(IEnumerable<ICommand> commands)
    {
        int oldCount = queue.Count;
        queue.AddRange(commands);
        if (oldCount == 0)
        {
            queue[0].Init();
        }
    }

    public void StopCurrentCommand()
    {
        if (queue.Count > 0)
        {
            queue[0].Cancel();
            Next();
        }
    }

    public void ClearCommands()
    {
        if (queue.Count > 0)
        {
            queue[0].Cancel();
            queue.Clear();
            OnBecomeIdle?.Invoke(this, null);
        }
    }

    public void Run()
    {
        if (queue.Count > 0 && queue[0].Execute() == ICommand.State.Stopped)
        {
            Next();
        }
    }

    private void Next()
    {
        queue.RemoveAt(0);
        if (queue.Count > 0)
        {
            queue[0].Init();
        }
        else
        {
            OnBecomeIdle?.Invoke(this, null);
        }
    }
}
