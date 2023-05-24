using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RetrieveItemTarget))]
public class AutoEmpty : JobDesignation
{
    private RetrieveItemTarget source;

    public override bool CanCreateJob()
    {
        return true;
    }

    public override bool TryCreateJob()
    {
        return false;
    }

    private bool IsHaulingJobForItem(IJob job, Item item)
    {
        if (job is not HaulItemJob)
        {
            return false;
        }
        HaulItemJob haulJob = (HaulItemJob)job;
        return haulJob.Source == source && haulJob.Item == item;
    }

    private void MaybeCreateJob(Item item)
    {
        if (!jobDispatcher.OpenJobs.Any(job => IsHaulingJobForItem(job, item)))
        {
            DepositItemTarget destination = FindObjectOfType<DepositItemTarget>();
            jobDispatcher.DispatchJob(new HaulItemJob(source, destination, item));
        }
    }

    private void OnInventoryAdd(Item item, int quantity)
    {
        MaybeCreateJob(item);
    }

    private void Awake()
    {
        source = GetComponent<RetrieveItemTarget>();
    }

    private void OnEnable()
    {
        source.Inventory.OnAdd.AddListener(OnInventoryAdd);
        foreach (ItemStack stack in source.Inventory.ItemStacks)
        {
            MaybeCreateJob(stack.Item);
        }
    }

    private void OnDisable()
    {
        source.Inventory.OnAdd.RemoveListener(OnInventoryAdd);
    }
}
