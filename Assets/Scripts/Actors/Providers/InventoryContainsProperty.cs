public class InventoryContainsProperty<TPropertyTable, TValue> : IProvider<bool> where TPropertyTable : PropertyTable<Item, TValue>
{
    public IProvider<Inventory> Inventory { get; private set; }
    public TPropertyTable PropertyTable { get; private set; }

    public InventoryContainsProperty(IProvider<Inventory> inventory, TPropertyTable propertyTable)
    {
        Inventory = inventory;
        PropertyTable = propertyTable;
    }

    public bool Get()
    {
        Inventory inventory = Inventory.Get();
        if (inventory == null)
        {
            return false;
        }
        foreach (ItemStack stack in inventory.ItemStacks)
        {
            if (PropertyTable.Entries.ContainsKey(stack.Item))
            {
                return true;
            }
        }
        return false;
    }
}
