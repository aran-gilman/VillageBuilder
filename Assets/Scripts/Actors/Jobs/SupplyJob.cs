using System.Collections.Generic;
using System.Linq;

public class SupplyJob : Job
{
    public IProvider<Inventory> Source { get; private set; }
    public IProvider<Inventory> Destination { get; private set; }
    public IProvider<Item> Item { get; private set; }
    public IProvider<int> TargetQuantity { get; private set; }

    private TransferItemsCommand itemPickUpCommand;
    private CompositeCommand haulCommand;

    public SupplyJob(JobDesignation owner, IProvider<Inventory> source, IProvider<Inventory> destination, IProvider<Item> item, IProvider<int> targetQuantity)
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

        ApproachCommand approachSourceCommand = new ApproachCommand(actor.NavMeshAgent, new TransformProvider<Inventory>(Source));
        itemPickUpCommand = new TransferItemsCommand(
            new ComponentProvider<Inventory>(approachSourceCommand.CachedTarget),
            actorInventoryProvider,
            Item,
            new IntDifference(TargetQuantity, inventoryCount));

        ApproachCommand approachDestinationCommand = new ApproachCommand(actor.NavMeshAgent, new TransformProvider<Inventory>(Destination));

        IEnumerable<ICommand> commands = new List<ICommand>
        {
            approachSourceCommand,
            itemPickUpCommand,
            approachDestinationCommand,
            new TransferItemsCommand(
                actorInventoryProvider,
                new ComponentProvider<Inventory>(approachDestinationCommand.CachedTarget),
                itemPickUpCommand.ActualItem,
                itemPickUpCommand.ActualQuantity)
        };
        haulCommand = new CompositeCommand(commands);
        RepeatCommand repeated = new RepeatCommand(haulCommand, new IsEqualProvider<int>(inventoryCount, TargetQuantity));
        return new CompositeCommand(new List<ICommand>() { repeated });
    }

    public override bool CanPerformWith(ActorAI actor)
    {
        return actor.NavMeshAgent != null && actor.Inventory != null;
    }

    public override ValidationResult IsValid()
    {
        if (Item.Get() == null || Destination.Get() == null || !Destination.Get().gameObject.activeSelf)
        {
            return ValidationResult.Impossible;
        }
        if (Source.Get() == null || !Source.Get().gameObject.activeSelf || Source.Get().Count(Item.Get()) == 0)
        {
            return ValidationResult.Wait;
        }
        return ValidationResult.Valid;
    }

    public override void Cancel()
    {
        base.Cancel();
        if (haulCommand != null && itemPickUpCommand.ActualItem.Get() != null && haulCommand.CommandRunner.History.Contains(itemPickUpCommand))
        {
            Assignee.Inventory.Drop(itemPickUpCommand.ActualItem.Get(), Assignee.transform);
        }
    }
}
