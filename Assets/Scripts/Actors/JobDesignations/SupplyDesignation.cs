using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SupplyDesignation : JobDesignation
{
    [SerializeField]
    private Inventory destination;

    [SerializeField]
    private SupplyList supplyList;
    public SupplyList SupplyList => supplyList;

    public void MaybeReactivateJob(ItemStack newStack)
    {
        foreach (ItemStack stack in supplyList.Items)
        {
            if (stack.Item == newStack.Item)
            {
                foreach (Job job in CurrentJobs.Where(j => j.Status == Job.JobStatus.Inactive))
                {
                    job.Status = Job.JobStatus.Available;
                }
                return;
            }
        }
    }

    public override bool CanCreateJobs()
    {
        foreach (ItemStack stack in supplyList.Items)
        {
            if (destination.Count(stack.Item) < stack.Quantity)
            {
                return true;
            }
        }
        return false;
    }

    protected override List<Job> CreateJobs()
    {
        List<Job> jobs = new List<Job>();
        IProvider<Inventory> inventoryProvider = new ConstProvider<Inventory>(destination);
        foreach (ItemStack stack in supplyList.Items)
        {
            if (destination.Count(stack.Item) < stack.Quantity)
            {
                IProvider<Item> itemProvider = new ConstProvider<Item>(stack.Item);
                jobs.Add(new SupplyJob(
                    this,
                    new NearestItemSource(new TransformProvider<Inventory>(inventoryProvider), itemProvider),
                    inventoryProvider,
                    itemProvider,
                    new ConstProvider<int>(stack.Quantity)));
            }
        }
        return jobs;
    }
}
