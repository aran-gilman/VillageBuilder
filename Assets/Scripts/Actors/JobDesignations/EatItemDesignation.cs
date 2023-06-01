using System.Collections.Generic;
using UnityEngine;

public class EatItemDesignation : JobDesignation
{
    [SerializeField]
    private ItemIntProperty restoreHungerProperty;

    public override bool CanCreateJobs()
    {
        return true;
    }

    protected override List<Job> CreateJobs()
    {
        return new List<Job>()
        {
            new EatItemJob(this, restoreHungerProperty)
        };
    }
}
