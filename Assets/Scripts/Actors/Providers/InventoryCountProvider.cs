using UnityEngine.Assertions;

public class InventoryCountProvider : IProvider<int>
{
    public Inventory Inventory { get; private set; }
    public Item Item { get; private set; }

    public InventoryCountProvider(Inventory inventory, Item item)
    {
        Assert.IsNotNull(inventory, "inventory must be non-null");
        Assert.IsNotNull(item, "item must be non-null");
        Inventory = inventory;
        Item = item;
    }

    public int Get()
    {
        // In case Inventory gets destroyed in between provider creation and now
        if (Inventory == null)
        {
            return 0;
        }
        return Inventory.Count(Item);
    }
}
