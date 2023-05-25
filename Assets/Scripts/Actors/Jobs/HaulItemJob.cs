using System.Collections.Generic;
using System.Linq;

public class HaulItemJob : Job
{
    public RetrieveItemTarget Source { get; private set; }
    public DepositItemTarget Destination { get; private set; }
    public Item Item { get; private set; }

    private ICommand itemPickUpCommand;

    public HaulItemJob(HaulDesignation owner, DepositItemTarget destination, Item item)
    {
        Owner = owner;
        Source = owner.Source;
        Destination = destination;
        Item = item;
        DisplayName = $"Haul {Item.name} from {Source.name} to {Destination.name}";
    }

    public override CompositeCommand CreateCommand(ActorAI actor)
    {
        itemPickUpCommand = new TransferItemsCommand(Source.Inventory, actor.Inventory, Item);
        IEnumerable<ICommand> commands = new List<ICommand>
        {
            new ApproachCommand(actor.NavMeshAgent, Source.transform),
            itemPickUpCommand,
            new ApproachCommand(actor.NavMeshAgent, Destination.transform),
            new TransferItemsCommand(actor.Inventory, Destination.Inventory, Item)
        };
        return new CompositeCommand(commands);
    }

    public override bool CanPerformWith(ActorAI actor)
    {
        return actor.NavMeshAgent != null && actor.Inventory != null;
    }

    public override bool IsValid()
    {
        bool isValid = true;
        isValid &= Source.gameObject.activeSelf;
        isValid &= Destination.gameObject.activeSelf;
        isValid &= Source.Inventory.Count(Item) > 0;
        return isValid;
    }

    public override void Cancel()
    {
        base.Cancel();
        if (command.CommandRunner.History.Contains(itemPickUpCommand))
        {
            Assignee.Inventory.Drop(Item, Assignee.transform);
        }
    }
}
