using System.Collections.Generic;

public class HaulItemJob : IJob
{
    public JobDesignation Owner { get; private set; }
    public RetrieveItemTarget Source { get; private set; }
    public DepositItemTarget Destination { get; private set; }
    public Item Item { get; private set; }

    public HaulItemJob(HaulDesignation owner, DepositItemTarget destination, Item item)
    {
        Owner = owner;
        Source = owner.Source;
        Destination = destination;
        Item = item;
    }

    public ICommand CreateCommand(ActorAI actor)
    {
        IEnumerable<ICommand> commands = new List<ICommand>
        {
            new ApproachCommand(actor.NavMeshAgent, Source.transform),
            new TransferItemsCommand(Source.Inventory, actor.Inventory, Item),
            new ApproachCommand(actor.NavMeshAgent, Destination.transform),
            new TransferItemsCommand(actor.Inventory, Destination.Inventory, Item)
        };
        return new CompositeCommand(commands);
    }

    public bool CanPerformWith(ActorAI actor)
    {
        return actor.NavMeshAgent != null && actor.Inventory != null;
    }

    public bool IsValid()
    {
        bool isValid = true;
        isValid &= Source.gameObject.activeSelf;
        isValid &= Destination.gameObject.activeSelf;
        isValid &= Source.Inventory.Count(Item) > 0;
        return isValid;
    }
}
