public class IsEqualProvider<T> : IProvider<bool>
{
    public IProvider<T> First { get; private set; }
    public IProvider<T> Second { get; private set; }

    public IsEqualProvider(IProvider<T> first, IProvider<T> second)
    {
        First = first;
        Second = second;
    }

    public bool Get()
    {
        return First.Get().Equals(Second.Get());
    }
}
