using System.Linq;
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
        var target = FindObjectOfType<ChopTarget>();
        commandRunner.AddCommand(new ApproachCommand(navMeshAgent, target.transform));
        commandRunner.AddCommand(new ChopCommand(target.Inventory));

        //Inventory inventory = GetComponent<Inventory>();
        //commandRunner.AddCommand(new RetrieveCommand(inventory, target.Inventory, target.Inventory.ItemStacks.First()));
    }
}
