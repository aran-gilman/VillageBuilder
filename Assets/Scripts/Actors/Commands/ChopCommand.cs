using UnityEngine;

public class ChopCommand : ICommand
{
    public Inventory TargetInventory { get; private set; }

    public ChopCommand(Inventory target)
    {
        TargetInventory = target;
    }

    public ICommand.State Execute()
    {
        TargetInventory.DropAll(TargetInventory.transform.position);
        Object.Destroy(TargetInventory.gameObject);
        return ICommand.State.Stopped;
    }

    public void Init()
    {
    }
}
