using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(RetrieveItemTarget))]
public class HaulDesignation : JobDesignation
{
    public RetrieveItemTarget Source { get; private set; }

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
        foreach (Item item in distinctItems)
        {
            jobs.Add(new HaulItemJob(this, destination, item));
        }
        return jobs;
    }

    private void OnInventoryAdd(Item item, int quantity)
    {
        if (!HasActiveJob())
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
