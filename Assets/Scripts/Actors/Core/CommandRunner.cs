using System.Collections.Generic;
using UnityEngine;

public class CommandRunner : MonoBehaviour
{
    private List<ICommand> queue = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        queue.Add(command);
        if (queue.Count == 1)
        {
            command.Init();
        }
    }

    public void StopCurrentCommand()
    {
        if (queue.Count > 0)
        {
            Next();
        }
    }

    private void FixedUpdate()
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
    }
}
