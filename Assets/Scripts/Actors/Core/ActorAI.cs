using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CommandRunner), typeof(NavMeshAgent))]
public class ActorAI : MonoBehaviour
{
    private CommandRunner commandRunner;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        commandRunner = GetComponent<CommandRunner>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // For testing only
        //var target = FindObjectOfType<ChopTarget>();
        //commandRunner.AddCommand(new ApproachCommand(navMeshAgent, target.transform));
        //commandRunner.AddCommand(new ChopCommand(target.Inventory));

        var target = GameObject.Find("Bush").GetComponentInChildren<RetrieveItemTarget>();
        var storage = FindObjectOfType<DepositItemTarget>();
        Inventory inventory = GetComponent<Inventory>();
        commandRunner.AddCommand(new ApproachCommand(navMeshAgent, target.transform));

        Item itemToRetrieve = target.Inventory.ItemStacks[0].Item;
        commandRunner.AddCommand(new TransferItemsCommand(inventory, target.Inventory, itemToRetrieve));
        commandRunner.AddCommand(new ApproachCommand(navMeshAgent, storage.transform));
        commandRunner.AddCommand(new TransferItemsCommand(storage.Inventory, inventory, itemToRetrieve));
    }
}
