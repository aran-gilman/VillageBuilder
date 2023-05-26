using System.Collections.Generic;
using System.Linq;

public class SupplyJob : Job
{
    public IProvider<RetrieveItemTarget> Source { get; private set; }
    public IProvider<Inventory> Destination { get; private set; }
    public IProvider<Item> Item { get; private set; }
    public IProvider<int> TargetQuantity { get; private set; }

    private ICommand itemPickUpCommand;
    private CompositeCommand haulCommand;

    public SupplyJob(JobDesignation owner, IProvider<RetrieveItemTarget> source, IProvider<Inventory> destination, IProvider<Item> item, IProvider<int> targetQuantity)
    {
        Owner = owner;
        Source = source;
        Destination = destination;
        Item = item;
        DisplayName = $"Supply items";
        TargetQuantity = targetQuantity;
    }

    public override CompositeCommand CreateCommand(ActorAI actor)
    {
        IProvider<Inventory> actorInventoryProvider = new ConstProvider<Inventory>(actor.Inventory);
        IProvider<int> inventoryCount = new InventoryCountProvider(Destination, Item);
        TransferItemsCommand cmd = new TransferItemsCommand(
            new ConstProvider<Inventory>(Source.Get().Inventory),
            actorInventoryProvider,
            Item,
            new IntDifference(TargetQuantity, inventoryCount));
        itemPickUpCommand = cmd;
        IEnumerable<ICommand> commands = new List<ICommand>
        {
            new ApproachCommand(actor.NavMeshAgent, Source.Get().transform),
            itemPickUpCommand,
            new ApproachCommand(actor.NavMeshAgent, Destination.Get().transform),
            new TransferItemsCommand(actorInventoryProvider, new ConstProvider<Inventory>(Destination.Get()), Item, cmd.TransferResult)
        };
        haulCommand = new CompositeCommand(commands);
        RepeatCommand repeated = new RepeatCommand(haulCommand, new IsEqualProvider<int>(inventoryCount, TargetQuantity));
        return new CompositeCommand(new List<ICommand>() { repeated });
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
        return Source.Get().Inventory.Count(Item.Get()) > 0;
    }

    public override void Cancel()
    {
        base.Cancel();
        if (Item.Get() != null && haulCommand.CommandRunner.History.Contains(itemPickUpCommand))
        {
            Assignee.Inventory.Drop(Item.Get(), Assignee.transform);
        }
    }
}
