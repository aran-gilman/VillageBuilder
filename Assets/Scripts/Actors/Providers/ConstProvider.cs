public class ConstProvider<T> : IProvider<T>
{
    private readonly T value;

    public ConstProvider(T value)
    {
        this.value = value;
    }

    public T Get()
    {
        return value;
    }
}
