using UnityEngine;

public class ChopCommand : ICommand
{
    public Inventory Target { get; private set; }

    public ChopCommand(Inventory target)
    {
        Target = target;
    }

    public ICommand.State Execute()
    {
        Target.DropAll(Target.transform.position);
        Object.Destroy(Target.gameObject);
        return ICommand.State.Stopped;
    }

    public void Init()
    {
    }
}
