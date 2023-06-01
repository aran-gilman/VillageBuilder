using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeItemCommand : ICommand
{
    public JobRunner Target { get; private set; }
    public ItemConsumer ItemConsumer { get; private set; }
    public IProvider<Item> Item { get; private set; }

    public ConsumeItemCommand(JobRunner target, ItemConsumer itemConsumer, IProvider<Item> item)
    {
        Target = target;
        ItemConsumer = itemConsumer;
        Item = item;
    }

    public ICommand.State Execute()
    {
        Item item = Item.Get();
        if (item == null)
        {
            return ICommand.State.Invalid;
        }
        int actualQuantity = Target.Inventory.Remove(item, 1);
        if (actualQuantity == 0)
        {
            return ICommand.State.Invalid;
        }
        ItemConsumer.ConsumeItem(item, Target.Motivator);
        return ICommand.State.Stopped;
    }
}
