using UnityEngine;

public class ChopDesignation : JobDesignation
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
        return false;
    }
}
