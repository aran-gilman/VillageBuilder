using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class JobDispatcher : ScriptableObject
{
    private List<Job> openJobs = new List<Job>();
    public IEnumerable<Job> OpenJobs => openJobs;

    public void DispatchJob(Job job)
    {
        openJobs.Add(job);
    }

    public bool AssignJob(Job job, ActorAI actor)
    {
        if (!job.IsValid())
        {
            openJobs.Remove(job);
            return false;
        }
        if (!job.CanPerformWith(actor))
        {
            return false;
        }
        openJobs.Remove(job);
        actor.CommandRunner.AddCommand(job.CreateCommand(actor));
        return true;
    }
}
