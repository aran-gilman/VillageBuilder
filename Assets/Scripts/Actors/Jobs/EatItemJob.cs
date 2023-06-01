using System.Collections.Generic;
using UnityEngine;

public class EatItemJob : Job
{
    public ItemIntProperty RestoreHungerProperty { get; private set; }

    public EatItemJob(JobDesignation owner, ItemIntProperty restoreHungerProperty)
    {
        Owner = owner;
        RestoreHungerProperty = restoreHungerProperty;
    }

    public override bool CanPerformWith(JobRunner actor)
    {
        return true;
    }

    public override CompositeCommand CreateCommand(JobRunner actor)
    {
        IProvider<Inventory> foodSource = new NearestItemWithProperty<ItemIntProperty, int>(new ConstProvider<Transform>(actor.transform), RestoreHungerProperty);
        ApproachCommand approachFoodSource = new ApproachCommand(actor.NavMeshAgent, new TransformProvider<Inventory>(foodSource));

        IProvider<Inventory> actorInventory = new ConstProvider<Inventory>(actor.Inventory);
        ConditionalCommand maybeGetFood = new ConditionalCommand(
            new Not(new InventoryContainsProperty<ItemIntProperty, int>(actorInventory, RestoreHungerProperty)),
            approachFoodSource);

        return new CompositeCommand(new List<ICommand>() { maybeGetFood });
    }

    public override ValidationResult IsValid()
    {
        return ValidationResult.Valid;
    }
}
