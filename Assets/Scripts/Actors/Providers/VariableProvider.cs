public class VariableProvider<T> : IProvider<T>
{
    public T Value { get; set; }

    public T Get() => Value;
}
