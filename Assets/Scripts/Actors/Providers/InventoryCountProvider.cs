using UnityEngine.Assertions;

public class InventoryCountProvider : IProvider<int>
{
    public IProvider<Inventory> Inventory { get; private set; }
    public IProvider<Item> Item { get; private set; }

    public InventoryCountProvider(IProvider<Inventory> inventory, IProvider<Item> item)
    {
        Assert.IsNotNull(inventory, "inventory must be non-null");
        Assert.IsNotNull(item, "item must be non-null");
        Inventory = inventory;
        Item = item;
    }

    public int Get()
    {
        // In case Inventory gets destroyed in between provider creation and now
        Inventory inventory = Inventory.Get();
        Item item = Item.Get();
        if (inventory == null || item == null)
        {
            return 0;
        }
        return inventory.Count(item);
    }
}
