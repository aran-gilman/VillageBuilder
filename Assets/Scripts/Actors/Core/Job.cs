using System;

public abstract class Job
{
    public event EventHandler OnJobCompleted;

    public enum JobStatus
    {
        Inactive,
        Available,
        Started,
        Completed
    }
    public JobStatus Status { get; set; }
    public ActorAI Assignee { get; set; }

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

    public abstract bool CanPerformWith(ActorAI actor);
    public abstract CompositeCommand CreateCommand(ActorAI actor);

    public bool Start()
    {
        ValidationResult isValid = IsValid();
        if (isValid == ValidationResult.Impossible)
        {
            Cancel();
            return false;
        }

        if (isValid == ValidationResult.Wait)
        {
            // TODO: Set up callbacks in JobDesignation to try re-activating the job when conditions are met
            Status = JobStatus.Inactive;
            return false;
        }

        if (!CanPerformWith(Assignee))
        {
            Assignee = null;
            return false;
        }

        Status = JobStatus.Started;
        command = CreateCommand(Assignee);
        command.CommandRunner.OnBecomeIdle += HandleCommandFinished;
        Assignee.CommandRunner.AddCommand(command);
        return true;
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