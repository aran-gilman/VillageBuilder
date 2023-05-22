public class RetrieveCommand : ICommand
{
    public Inventory Destination { get; private set; }
    public Inventory Source { get; private set; }
    public ItemStack Items { get; private set; }

    public RetrieveCommand(Inventory destination, Inventory source, ItemStack items)
    {
        Destination = destination;
        Source = source;
        Items = items;
    }

    public ICommand.State Execute()
    {
        int retrieved = Source.Remove(Items);
        if (retrieved == Items.Quantity)
        {
            Destination.Add(Items);
        }
        else
        {
            Destination.Add(Items.Item, retrieved);
        }
        return ICommand.State.Stopped;
    }

    public void Init()
    {
    }
}
