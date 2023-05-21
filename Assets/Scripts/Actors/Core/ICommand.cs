public interface ICommand
{
    public enum State
    {
        Running,
        Stopped
    }

    void Init();
    State Execute();
}
