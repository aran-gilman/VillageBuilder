public class TransferItemsCommand : ICommand
{
    public IProvider<Inventory> Destination { get; private set; }
    public IProvider<Inventory> Source { get; private set; }
    public IProvider<Item> Item { get; private set; }
    public IProvider<int> Quantity { get; private set; }

    private VariableProvider<int> transferResult = new VariableProvider<int>();
    public IProvider<int> TransferResult => transferResult;

    public TransferItemsCommand(IProvider<Inventory> source, IProvider<Inventory> destination, IProvider<Item> item, IProvider<int> quantity)
    {
        Destination = destination;
        Source = source;
        Item = item;
        Quantity = quantity;
    }

    public ICommand.State Execute()
    {
        int quantityToRetrieve = Quantity.Get();
        Item item = Item.Get();
        Inventory source = Source.Get();
        Inventory destination = Destination.Get();
        if (quantityToRetrieve <= 0 || item == null || source == null || destination == null)
        {
            return ICommand.State.Invalid;
        }
        int actualQuantity = source.Remove(item, quantityToRetrieve);
        transferResult.Value = actualQuantity;
        if (actualQuantity <= 0)
        {
            return ICommand.State.Invalid;
        }
        destination.Add(item, actualQuantity);
        return ICommand.State.Stopped;
    }
}
