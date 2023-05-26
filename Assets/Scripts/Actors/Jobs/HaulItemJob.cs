using System.Collections.Generic;
using System.Linq;

public class HaulItemJob : Job
{
    public IProvider<RetrieveItemTarget> Source { get; private set; }
    public IProvider<DepositItemTarget> Destination { get; private set; }
    public IProvider<Item> Item { get; private set; }
    public IProvider<int> Quantity { get; private set; }

    private ICommand itemPickUpCommand;

    public HaulItemJob(JobDesignation owner, IProvider<RetrieveItemTarget> source, IProvider<DepositItemTarget> destination, IProvider<Item> item, IProvider<int> quantity)
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
        TransferItemsCommand cmd = new TransferItemsCommand(new ConstProvider<Inventory>(Source.Get().Inventory), actorInventoryProvider, Item, Quantity);
        itemPickUpCommand = cmd;
        IEnumerable<ICommand> commands = new List<ICommand>
        {
            new ApproachCommand(actor.NavMeshAgent, Source.Get().transform),
            itemPickUpCommand,
            new ApproachCommand(actor.NavMeshAgent, Destination.Get().transform),
            new TransferItemsCommand(actorInventoryProvider, new ConstProvider<Inventory>(Destination.Get().Inventory), Item, cmd.TransferResult)
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
        return Source.Get().Inventory.Count(Item.Get()) > 0;
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
