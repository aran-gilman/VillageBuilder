using System;
using System.Collections.Generic;
using System.Linq;

public class JobDispatcher
{
    private static JobDispatcher instance;
    public static JobDispatcher Get()
    {
        if (instance == null)
        {
            instance = new JobDispatcher();
        }
        return instance;
    }

    public event EventHandler<Job> OnJobDispatched;

    private List<Job> allJobs = new List<Job>();
    public IEnumerable<Job> AllJobs => allJobs;
    
    // TODO: Sort by priority
    public IEnumerable<Job> GetAvailableJobs(JobRunner actor)
    {
        return allJobs.Where(job => IsJobAvailableToActor(actor, job));
    }

    public void DispatchJob(Job job)
    {
        job.Status = Job.JobStatus.Available;
        job.OnJobCompleted += HandleJobCompleted;
        allJobs.Add(job);
        OnJobDispatched?.Invoke(this, job);
    }

    private void HandleJobCompleted(object sender, object args)
    {
        Job job = (Job)sender;
        job.OnJobCompleted -= HandleJobCompleted;
        allJobs.Remove(job);
    }

    private bool IsJobAvailableToActor(JobRunner actor, Job job)
    {
        if (job.Status != Job.JobStatus.Available)
        {
            return false;
        }
        if (job.Assignee != null && job.Assignee != actor)
        {
            return false;
        }
        return job.CanPerformWith(actor);
    }

    private JobDispatcher() { }
}
