public class Not : IProvider<bool>
{
    public IProvider<bool> Input { get; private set; }

    public Not(IProvider<bool> input)
    {
        Input = input;
    }

    public bool Get()
    {
        return !Input.Get();
    }
}
