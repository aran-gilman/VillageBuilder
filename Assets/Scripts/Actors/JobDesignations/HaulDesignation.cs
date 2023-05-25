using UnityEngine;

[RequireComponent(typeof(RetrieveItemTarget))]
public class HaulDesignation : JobDesignation
{
    public RetrieveItemTarget Source { get; private set; }

    public override bool CanCreateJob()
    {
        return Source.Inventory.ItemStacks.Count > 0 && FindObjectOfType<DepositItemTarget>() != null;
    }

    protected override Job CreateJob()
    {
        if (Source.Inventory.ItemStacks.Count == 0)
        {
            return null;
        }
        Item item = Source.Inventory.ItemStacks[0].Item;
        DepositItemTarget destination = FindObjectOfType<DepositItemTarget>();
        if (destination == null)
        {
            return null;
        }
        return new HaulItemJob(this, destination, item);
    }

    private void OnInventoryAdd(Item item, int quantity)
    {
        if (CurrentJob == null)
        {
            DispatchJob();
        }
    }

    private void Awake()
    {
        Source = GetComponent<RetrieveItemTarget>();
    }

    private void OnEnable()
    {
        Source.Inventory.OnAdd.AddListener(OnInventoryAdd);
        DispatchJob();
    }

    private void OnDisable()
    {
        Source.Inventory.OnAdd.RemoveListener(OnInventoryAdd);
    }
}
