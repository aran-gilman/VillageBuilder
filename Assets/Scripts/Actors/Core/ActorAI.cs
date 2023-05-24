using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ActorAI : MonoBehaviour
{
    [SerializeField]
    private JobDispatcher jobDispatcher;

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
            IJob job = jobDispatcher.OpenJobs.FirstOrDefault();
            if (job != null)
            {
                jobDispatcher.AssignJob(job, this);
            }
        }
    }
}
