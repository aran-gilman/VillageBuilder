public class TransferItemsCommand : ICommand
{
    public IProvider<Inventory> Destination { get; private set; }
    public IProvider<Inventory> Source { get; private set; }
    public IProvider<Item> Item { get; private set; }
    public IProvider<int> Quantity { get; private set; }

    private VariableProvider<int> actualQuantity = new VariableProvider<int>();
    public IProvider<int> ActualQuantity => actualQuantity;

    private VariableProvider<Item> actualItem = new VariableProvider<Item>();
    public IProvider<Item> ActualItem => actualItem;

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
        actualItem.Value = Item.Get();
        Inventory source = Source.Get();
        Inventory destination = Destination.Get();
        if (quantityToRetrieve <= 0 || actualItem.Value == null || source == null || destination == null)
        {
            return ICommand.State.Invalid;
        }
        actualQuantity.Value = source.Remove(actualItem.Value, quantityToRetrieve);
        if (actualQuantity.Value <= 0)
        {
            return ICommand.State.Invalid;
        }
        destination.Add(actualItem.Value, actualQuantity.Value);
        return ICommand.State.Stopped;
    }
}
