using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class JobRunner : MonoBehaviour
{
    public CommandRunner CommandRunner { get; } = new CommandRunner();
    public NavMeshAgent NavMeshAgent { get; private set; }
    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Inventory = GetComponent<Inventory>();

        CommandRunner.OnBecomeIdle += HandleBecomeIdle;
        JobDispatcher.Get().OnJobDispatched += HandleJobDispatched;
    }

    private void OnDestroy()
    {
        CommandRunner.OnBecomeIdle -= HandleBecomeIdle;
        JobDispatcher.Get().OnJobDispatched -= HandleJobDispatched;
    }

    private void Update()
    {
        CommandRunner.Run();
    }

    private void HandleBecomeIdle(object sender, object args)
    {
        Job job = JobDispatcher.Get().GetAvailableJobs(this).FirstOrDefault();
        if (job != null)
        {
            job.Assignee = this;
            job.Start();
        }
    }

    private void HandleJobDispatched(object sender, Job job)
    {
        if (CommandRunner.IsIdle && job.Assignee == null && job.CanPerformWith(this))
        {
            job.Assignee = this;
            job.Start();
        }
    }
}
