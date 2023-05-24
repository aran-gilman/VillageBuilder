using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CommandRunner), typeof(NavMeshAgent))]
public class ActorAI : MonoBehaviour
{
    [SerializeField]
    private JobDispatcher jobDispatcher;

    public CommandRunner CommandRunner { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        CommandRunner = GetComponent<CommandRunner>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Inventory = GetComponent<Inventory>();

        // For testing only
        /*
        var target = FindObjectOfType<ChopTarget>();
        CommandRunner.AddCommand(new ApproachCommand(NavMeshAgent, target.transform));
        CommandRunner.AddCommand(new ChopCommand(target.Inventory));
        */
    }

    private void Update()
    {
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
