using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class JobDispatcher : ScriptableObject
{
    private List<Job> allJobs = new List<Job>();
    public IEnumerable<Job> AllJobs => allJobs;
    public IEnumerable<Job> OpenJobs => allJobs.Where(job => job.Status == Job.JobStatus.Unassigned);

    public void DispatchJob(Job job)
    {
        allJobs.Add(job);
    }

    public bool AssignJob(Job job, ActorAI actor)
    {
        if (!job.IsValid())
        {
            allJobs.Remove(job);
            return false;
        }
        if (!job.CanPerformWith(actor))
        {
            return false;
        }
        job.Status = Job.JobStatus.Assigned;
        actor.CommandRunner.AddCommand(job.CreateCommand(actor));
        return true;
    }
}
