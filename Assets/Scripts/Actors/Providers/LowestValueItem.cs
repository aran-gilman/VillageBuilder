using UnityEngine;

public class LowestValueItem : IProvider<Item>
{
    public IProvider<Inventory> Inventory { get; private set; }
    public ItemIntProperty Property { get; private set; }

    public LowestValueItem(IProvider<Inventory> inventory, ItemIntProperty property)
    {
        Inventory = inventory;
        Property = property;
    }

    public Item Get()
    {
        Inventory inventory = Inventory.Get();
        if (inventory == null)
        {
            return null;
        }
        float lowestValue = Mathf.Infinity;
        Item lowestValueItem = null;
        foreach (ItemStack stack in inventory.ItemStacks)
        {
            if (Property.Entries.TryGetValue(stack.Item, out int value) && value < lowestValue)
            {
                lowestValue = value;
                lowestValueItem = stack.Item;
            }
        }
        return lowestValueItem;
    }
}
