using UnityEngine;

public class DestroyDesignation : JobDesignation
{
    [SerializeField]
    private Inventory inventory;
    public Inventory Inventory => inventory;

    public override bool CanCreateJob()
    {
        return true;
    }

    public override bool TryCreateJob()
    {
        jobDispatcher.DispatchJob(new DestroyJob(this));
        return true;
    }
}
