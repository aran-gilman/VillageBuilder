using System.Collections.Generic;

public class DestroyDesignation : JobDesignation
{
    public override bool CanCreateJobs()
    {
        return true;
    }

    protected override List<Job> CreateJobs()
    {
        return new List<Job>()
        {
            new DestroyJob(this)
        };
    }
}
