using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class JobDispatcher : ScriptableObject
{
    private List<Job> allJobs = new List<Job>();
    public IEnumerable<Job> AllJobs => allJobs;
    public IEnumerable<Job> OpenJobs => allJobs.Where(job => job.Status == Job.JobStatus.Available);

    public void DispatchJob(Job job)
    {
        job.Status = Job.JobStatus.Available;
        allJobs.Add(job);
    }

    public void Cancel(Job job)
    {
        allJobs.Remove(job);
        job.Assignee.CommandRunner.StopCurrentCommand();
    }

    public bool AssignJob(Job job, ActorAI actor)
    {
        job.Assignee = actor;
        return job.Start();
    }
}
