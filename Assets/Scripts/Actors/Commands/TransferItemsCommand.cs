public class TransferItemsCommand : ICommand
{
    public Inventory Destination { get; private set; }
    public Inventory Source { get; private set; }
    public Item Item { get; private set; }
    public IProvider<int> Quantity { get; private set; }

    public class TransferResultProvider : IProvider<int>
    {
        public int Value { get; set; }

        public int Get() => Value;
    }
    public TransferResultProvider TransferResult { get; } = new TransferResultProvider();

    public TransferItemsCommand(Inventory source, Inventory destination, Item item, IProvider<int> quantity = null)
    {
        Destination = destination;
        Source = source;
        Item = item;
        
        if (quantity == null)
        {
            Quantity = new InventoryCountProvider(Source, Item);
        }
        else
        {
            Quantity = quantity;
        }
    }

    public ICommand.State Execute()
    {
        int quantityToRetrieve = Quantity.Get();
        if (quantityToRetrieve > 0)
        {
            int actualQuantity = Source.Remove(Item, Quantity.Get());
            Destination.Add(Item, actualQuantity);
            TransferResult.Value = actualQuantity;
        }
        return ICommand.State.Stopped;
    }
}
