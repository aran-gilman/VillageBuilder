using UnityEngine;

public class DestroyDesignation : JobDesignation
{
    [SerializeField]
    private GameObjectGameEvent destroyEvent;

    public override bool CanCreateJob()
    {
        return true;
    }

    public override bool TryCreateJob()
    {
        jobDispatcher.DispatchJob(new DestroyJob(this, destroyEvent));
        return true;
    }
}
