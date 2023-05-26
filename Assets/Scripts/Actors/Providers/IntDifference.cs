public class IntDifference : IProvider<int>
{
    public IProvider<int> First { get; private set; }
    public IProvider<int> Second { get; private set; }

    public IntDifference(IProvider<int> first, IProvider<int> second)
    {
        First = first;
        Second = second;
    }

    public int Get() => First.Get() - Second.Get();
}
