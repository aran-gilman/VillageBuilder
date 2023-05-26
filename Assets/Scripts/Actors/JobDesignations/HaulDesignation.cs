using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RetrieveItemTarget))]
public class HaulDesignation : JobDesignation
{
    private RetrieveItemTarget source;
    public RetrieveItemTarget Source
    {
        get
        {
            if (source == null)
            {
                source = GetComponent<RetrieveItemTarget>();
            }
            return source;
        }
    }

    public override bool CanCreateJobs()
    {
        return Source.Inventory.ItemStacks.Count > 0 && FindObjectOfType<DepositItemTarget>() != null;
    }

    protected override List<Job> CreateJobs()
    {
        List<Job> jobs = new List<Job>();
        DepositItemTarget destination = FindObjectOfType<DepositItemTarget>();
        if (destination == null)
        {
            return jobs;
        }
        IEnumerable<Item> distinctItems = Source.Inventory.ItemStacks.Select(stack => stack.Item).Distinct();
        IProvider<Inventory> sourceProvider = new ConstProvider<Inventory>(source.Inventory);
        IProvider<Inventory> destinationProvider = new ConstProvider<Inventory>(destination.Inventory);
        foreach (Item item in distinctItems)
        {
            jobs.Add(new HaulItemJob(this, sourceProvider, destinationProvider, new ConstProvider<Item>(item), new InventoryCountProvider(source.Inventory, item)));
        }
        return jobs;
    }
}
