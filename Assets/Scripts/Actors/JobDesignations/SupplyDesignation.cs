using System.Collections.Generic;
using UnityEngine;

public class SupplyDesignation : JobDesignation
{
    [SerializeField]
    private Inventory destination;

    [SerializeField]
    private SupplyList supplyList;

    public override bool CanCreateJobs()
    {
        return true;
    }

    protected override List<Job> CreateJobs()
    {
        List<Job> jobs = new List<Job>();
        IProvider<Inventory> inventoryProvider = new ConstProvider<Inventory>(destination);
        foreach (ItemStack stack in supplyList.Items)
        {
            IProvider<Item> itemProvider = new ConstProvider<Item>(stack.Item);
            jobs.Add(new SupplyJob(
                this,
                new NearestItemSource(new GameObjectPositionProvider(new ConstProvider<GameObject>(destination.gameObject)), itemProvider),
                inventoryProvider,
                itemProvider,
                new ConstProvider<int>(stack.Quantity)));
        }
        return jobs;
    }
}
