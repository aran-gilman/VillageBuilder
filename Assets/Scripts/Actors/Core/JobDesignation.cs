using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class JobDesignation : MonoBehaviour
{
    [SerializeField]
    protected string displayName;
    public string DisplayName => displayName;

    [SerializeField]
    private UnityEvent onAllJobsCompleted;
    public UnityEvent OnAllJobsCompleted => onAllJobsCompleted;

    private List<Job> currentJobs = new List<Job>();
    public IEnumerable<Job> CurrentJobs => currentJobs;

    public abstract bool CanCreateJobs();
    protected abstract List<Job> CreateJobs();

    public bool HasActiveJob()
    {
        return currentJobs.Count > 0;
    }

    // TODO: Check for any missing or inactive jobs and re-dispatch/activate them if necessary
    public void DispatchJob()
    {
        if (currentJobs.Count > 0)
        {
            return;
        }
        currentJobs = CreateJobs();
        foreach (Job job in currentJobs)
        {
            job.OnJobCompleted += HandleJobCompleted;
            JobDispatcher.Get().DispatchJob(job);
        }
    }

    private void HandleJobCompleted(object sender, object args)
    {
        Job job = (Job)sender;
        job.OnJobCompleted -= HandleJobCompleted;
        if (!currentJobs.Any(j => j.Status != Job.JobStatus.Completed))
        {
            onAllJobsCompleted.Invoke();
            currentJobs.Clear();
        }
    }
}
