using System.Collections.Generic;
using UnityEngine;

public class EatItemJob : Job
{
    public ItemConsumer ItemConsumer { get; private set; }
    public Motive TargetMotive { get; private set; }
    public ItemIntProperty ItemProperty { get; private set; }

    public EatItemJob(JobDesignation owner, ItemConsumer itemConsumer, Motive targetMotive)
    {
        Owner = owner;
        ItemConsumer = itemConsumer;
        TargetMotive = targetMotive;
        ItemProperty = ItemConsumer.GetProperty(TargetMotive);
    }

    public override bool CanPerformWith(JobRunner actor)
    {
        return true;
    }

    public override CompositeCommand CreateCommand(JobRunner actor)
    {
        IProvider<Inventory> actorInventory = new ConstProvider<Inventory>(actor.Inventory);
        NearestItemWithProperty<ItemIntProperty, int> itemSearch = new NearestItemWithProperty<ItemIntProperty, int>(new ConstProvider<Transform>(actor.transform), ItemProperty);

        ApproachCommand approachFoodSource = new ApproachCommand(actor.NavMeshAgent, new TransformProvider<Inventory>(itemSearch));
        TransferItemsCommand transferItemsCommand = new TransferItemsCommand(itemSearch, actorInventory, new LowestValueItem(itemSearch, ItemProperty), new ConstProvider<int>(1));

        ConditionalCommand maybeGetFood = new ConditionalCommand(
            new Not(new InventoryContainsProperty<ItemIntProperty, int>(actorInventory, ItemProperty)),
            new CompositeCommand(new List<ICommand>() { approachFoodSource, transferItemsCommand }));

        ConsumeItemCommand consumeItem = new ConsumeItemCommand(actor, ItemConsumer, new LowestValueItem(actorInventory, ItemProperty));

        return new CompositeCommand(new List<ICommand>() { maybeGetFood, consumeItem });
    }

    public override ValidationResult IsValid()
    {
        return ValidationResult.Valid;
    }
}
