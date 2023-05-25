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
        job.OnJobCompleted += HandleJobCompleted;
        allJobs.Add(job);
    }

    public bool AssignJob(Job job, ActorAI actor)
    {
        job.Assignee = actor;
        return job.Start();
    }

    private void HandleJobCompleted(object sender, object args)
    {
        Job job = (Job)sender;
        job.OnJobCompleted -= HandleJobCompleted;
        allJobs.Remove(job);
    }
}
