public class DestroyCommand : ICommand
{
    public WorldObjectLifecycleManager ObjectToDestroy { get; private set; }

    public DestroyCommand(WorldObjectLifecycleManager objectToDestroy)
    {
        ObjectToDestroy = objectToDestroy;
    }

    public ICommand.State Execute()
    {
        ObjectToDestroy.DoDestroy();
        return ICommand.State.Stopped;
    }
}
