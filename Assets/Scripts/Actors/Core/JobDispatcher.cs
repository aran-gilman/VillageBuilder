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

    public void Cancel(Job job)
    {
        allJobs.Remove(job);
        job.Assignee.CommandRunner.StopCurrentCommand();
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
        job.Assignee = actor;
        actor.CommandRunner.AddCommand(job.CreateCommand(actor));
        return true;
    }
}
