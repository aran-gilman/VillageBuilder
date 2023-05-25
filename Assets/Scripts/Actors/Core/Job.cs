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

    private CompositeCommand command;

    public abstract bool CanPerformWith(ActorAI actor);
    public abstract bool IsValid();
    public abstract CompositeCommand CreateCommand(ActorAI actor);

    public bool Start()
    {
        if (!IsValid() || !CanPerformWith(Assignee))
        {
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
        Assignee.CommandRunner.StopCurrentCommand();
    }

    private void HandleCommandFinished(object sender, object args)
    {
        command.CommandRunner.OnBecomeIdle -= HandleCommandFinished;
        Status = JobStatus.Completed;
        OnJobCompleted?.Invoke(this, null);
    }
}