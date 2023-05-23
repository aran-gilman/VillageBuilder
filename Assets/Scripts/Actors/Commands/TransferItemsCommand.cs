public class TransferItemsCommand : ICommand
{
    public const int kTransferAll = -1;

    public Inventory Destination { get; private set; }
    public Inventory Source { get; private set; }
    public Item Item { get; private set; }
    public int Quantity { get; private set; }

    public TransferItemsCommand(Inventory source, Inventory destination, Item item, int quantity = kTransferAll)
    {
        Destination = destination;
        Source = source;
        Item = item;
        Quantity = quantity;
    }

    public ICommand.State Execute()
    {
        int quantityRetrieved;
        if (Quantity == kTransferAll)
        {
            quantityRetrieved = Source.RemoveAll(Item);
        }
        else
        {
            quantityRetrieved = Source.Remove(Item, Quantity);
        }
        Destination.Add(Item, quantityRetrieved);
        return ICommand.State.Stopped;
    }

    public void Init()
    {
    }
}
