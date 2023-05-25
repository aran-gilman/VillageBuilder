using System.Collections.Generic;
using UnityEngine;

public class DestroyDesignation : JobDesignation
{
    [SerializeField]
    private GameObjectGameEvent destroyEvent;

    public override bool CanCreateJobs()
    {
        return true;
    }

    protected override List<Job> CreateJobs()
    {
        return new List<Job>()
        {
            new DestroyJob(this, destroyEvent)
        };
    }
}
