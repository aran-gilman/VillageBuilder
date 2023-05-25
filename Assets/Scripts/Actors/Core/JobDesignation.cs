using UnityEngine;

public abstract class JobDesignation : MonoBehaviour
{
    [SerializeField]
    protected JobDispatcher jobDispatcher;

    [SerializeField]
    protected string displayName;

    public string DisplayName => displayName;

    public Job CurrentJob { get; private set; }

    public void DispatchJob()
    {
        if (CurrentJob != null)
        {
            return;
        }
        CurrentJob = CreateJob();
        if (CurrentJob != null)
        {
            CurrentJob.OnJobCompleted += HandleJobCompleted;
            jobDispatcher.DispatchJob(CurrentJob);
        }
    }

    public abstract bool CanCreateJob();
    protected abstract Job CreateJob();

    private void HandleJobCompleted(object sender, object args)
    {
        CurrentJob.OnJobCompleted -= HandleJobCompleted;
        CurrentJob = null;
    }
}
