using System;
using System.Collections.Generic;

public class CommandRunner
{
    public event EventHandler OnBecomeIdle;
    public event EventHandler OnInvalidCommand;

    public bool IsIdle => queue.Count == 0;

    private List<ICommand> queue = new List<ICommand>();
    public IEnumerable<ICommand> Queue => queue;

    private List<ICommand> history = new List<ICommand>();
    public IEnumerable<ICommand> History => history;

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
        if (queue.Count == 0)
        {
            return;
        }
        ICommand.State state = queue[0].Execute();
        switch (state)
        {
            case ICommand.State.Running:
                break;
            case ICommand.State.Stopped:
                Next();
                break;
            case ICommand.State.Invalid:
                OnInvalidCommand?.Invoke(this, null);
                Next();
                break;
        }
    }

    public void ClearHistory()
    {
        history.Clear();
    }

    private void Next()
    {
        if (queue.Count == 0)
        {
            return;
        }
        history.Add(queue[0]);
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
