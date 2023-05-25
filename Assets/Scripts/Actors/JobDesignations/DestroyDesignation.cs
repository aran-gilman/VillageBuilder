using UnityEngine;

public class DestroyDesignation : JobDesignation
{
    [SerializeField]
    private GameObjectGameEvent destroyEvent;

    public override bool CanCreateJob()
    {
        return true;
    }

    protected override Job CreateJob()
    {
        return new DestroyJob(this, destroyEvent);
    }
}
