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
        var target = FindObjectOfType<ChopTarget>();
        CommandRunner.AddCommand(new ApproachCommand(NavMeshAgent, target.transform));
        CommandRunner.AddCommand(new ChopCommand(target.Inventory));

        /*
        var target = GameObject.Find("Bush").GetComponentInChildren<RetrieveItemTarget>();
        var storage = FindObjectOfType<DepositItemTarget>();
        CommandRunner.AddCommand(new ApproachCommand(NavMeshAgent, target.transform));

        Item itemToRetrieve = target.Inventory.ItemStacks[0].Item;
        CommandRunner.AddCommand(new TransferItemsCommand(Inventory, target.Inventory, itemToRetrieve));
        CommandRunner.AddCommand(new ApproachCommand(NavMeshAgent, storage.transform));
        CommandRunner.AddCommand(new TransferItemsCommand(storage.Inventory, Inventory, itemToRetrieve));
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
