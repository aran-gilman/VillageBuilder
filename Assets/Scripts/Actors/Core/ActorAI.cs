using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ActorAI : MonoBehaviour
{
    public CommandRunner CommandRunner { get; } = new CommandRunner();
    public NavMeshAgent NavMeshAgent { get; private set; }
    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        CommandRunner.Run();
        if (CommandRunner.IsIdle)
        {
            Job job = JobDispatcher.Get().OpenJobs.FirstOrDefault();
            if (job != null)
            {
                JobDispatcher.Get().AssignJob(job, this);
            }
        }
    }
}
