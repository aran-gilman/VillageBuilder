using System;
using UnityEngine;

public abstract class Job
{
    public event EventHandler OnJobCompleted;
    public event EventHandler OnJobInactive;
    public event EventHandler OnJobStarted;

    public enum JobStatus
    {
        Inactive,
        Available,
        Started,
        Completed
    }
    public JobStatus Status { get; set; }
    public JobRunner Assignee { get; set; }

    public string DisplayName { get; protected set; }
    public JobDesignation Owner { get; protected set; }

    protected CompositeCommand command;

    public enum ValidationResult
    {
        Valid,
        Wait,
        Impossible
    }
    public abstract ValidationResult IsValid();

    public abstract bool CanPerformWith(JobRunner actor);
    public abstract CompositeCommand CreateCommand(JobRunner actor);

    public void Start()
    {
        ValidationResult isValid = IsValid();
        if (isValid == ValidationResult.Impossible)
        {
            Cancel();
            return;
        }

        if (isValid == ValidationResult.Wait)
        {
            Status = JobStatus.Inactive;
            OnJobInactive?.Invoke(this, null);
            return;
        }
        Status = JobStatus.Started;
        command = CreateCommand(Assignee);
        command.CommandRunner.OnBecomeIdle += HandleCommandFinished;
        Assignee.CommandRunner.AddCommand(command);
        OnJobStarted?.Invoke(this, null);
    }

    public virtual void Cancel()
    {
        if (Assignee != null && Status == JobStatus.Started)
        {
            Assignee.CommandRunner.StopCurrentCommand();
        }
        else
        {
            // TODO: Properly implement inactive and/or canceled status
            Status = JobStatus.Completed;
            OnJobCompleted?.Invoke(this, null);
        }
    }

    private void HandleCommandFinished(object sender, object args)
    {
        command.CommandRunner.OnBecomeIdle -= HandleCommandFinished;
        Status = JobStatus.Completed;
        OnJobCompleted?.Invoke(this, null);
    }
}