using System.Collections;
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
        foreach (ItemStack stack in supplyList.Items)
        {
            // Create haul item job
        }
        return jobs;
    }
}
