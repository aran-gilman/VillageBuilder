using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class JobDispatcher : ScriptableObject
{
    private List<IJob> openJobs = new List<IJob>();
    public IEnumerable<IJob> OpenJobs => openJobs;

    public void DispatchJob(IJob job)
    {
        openJobs.Add(job);
    }

    public bool AssignJob(IJob job, ActorAI actor)
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
        actor.CommandRunner.AddCommands(job.CreateCommands(actor));
        return true;
    }
}
