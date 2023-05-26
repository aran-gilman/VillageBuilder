using System.Collections.Generic;
using System.Linq;

public class HaulItemJob : Job
{
    public IProvider<Inventory> Source { get; private set; }
    public IProvider<Inventory> Destination { get; private set; }
    public IProvider<Item> Item { get; private set; }
    public IProvider<int> Quantity { get; private set; }

    private ICommand itemPickUpCommand;

    public HaulItemJob(JobDesignation owner, IProvider<Inventory> source, IProvider<Inventory> destination, IProvider<Item> item, IProvider<int> quantity)
    {
        Owner = owner;
        Source = source;
        Destination = destination;
        Item = item;
        DisplayName = $"Haul item";
        Quantity = quantity;
    }

    public override CompositeCommand CreateCommand(ActorAI actor)
    {
        IProvider<Inventory> actorInventoryProvider = new ConstProvider<Inventory>(actor.Inventory);
        TransferItemsCommand cmd = new TransferItemsCommand(Source, actorInventoryProvider, Item, Quantity);
        itemPickUpCommand = cmd;
        IEnumerable<ICommand> commands = new List<ICommand>
        {
            new ApproachCommand(actor.NavMeshAgent, new TransformPositionProvider<Inventory>(Source)),
            itemPickUpCommand,
            new ApproachCommand(actor.NavMeshAgent, new TransformPositionProvider<Inventory>(Destination)),
            new TransferItemsCommand(actorInventoryProvider, Destination, Item, cmd.TransferResult)
        };
        return new CompositeCommand(commands);
    }

    public override bool CanPerformWith(ActorAI actor)
    {
        return actor.NavMeshAgent != null && actor.Inventory != null;
    }

    public override bool IsValid()
    {
        if (Item.Get() == null || Source.Get() == null || Destination.Get() == null)
        {
            return false;
        }
        if (!Source.Get().gameObject.activeSelf || !Destination.Get().gameObject.activeSelf)
        {
            return false;
        }
        return Source.Get().Count(Item.Get()) > 0;
    }

    public override void Cancel()
    {
        base.Cancel();
        if (Item.Get() != null && command.CommandRunner.History.Contains(itemPickUpCommand))
        {
            Assignee.Inventory.Drop(Item.Get(), Assignee.transform);
        }
    }
}
