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
        if (quantityToRetrieve > 0)
        {
            int actualQuantity = Source.Get().Remove(Item.Get(), Quantity.Get());
            Destination.Get().Add(Item.Get(), actualQuantity);
            transferResult.Value = actualQuantity;
        }
        return ICommand.State.Stopped;
    }
}
