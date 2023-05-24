using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RetrieveItemTarget))]
public class HaulDesignation : JobDesignation
{
    private RetrieveItemTarget source;

    public override bool CanCreateJob()
    {
        return source.Inventory.ItemStacks.Count > 0 && FindObjectOfType<DepositItemTarget>() != null;
    }

    public override bool TryCreateJob()
    {
        bool didCreateJob = false;
        foreach (ItemStack stack in source.Inventory.ItemStacks)
        {
            didCreateJob |= MaybeCreateJob(stack.Item);
        }
        return didCreateJob;
    }

    private bool IsHaulingJobForItem(Job job, Item item)
    {
        if (job is not HaulItemJob)
        {
            return false;
        }
        HaulItemJob haulJob = (HaulItemJob)job;
        return haulJob.Source == source && haulJob.Item == item;
    }

    private bool MaybeCreateJob(Item item)
    {
        if (!jobDispatcher.OpenJobs.Any(job => IsHaulingJobForItem(job, item)))
        {
            DepositItemTarget destination = FindObjectOfType<DepositItemTarget>();
            if (destination != null)
            {
                jobDispatcher.DispatchJob(new HaulItemJob(source, destination, item));
                return true;
            }
        }
        return false;
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
