public interface ICommand
{
    public enum State
    {
        Running,
        Stopped,
        Invalid
    }

    void Init() { }
    State Execute();
    void Cancel() { }
}
