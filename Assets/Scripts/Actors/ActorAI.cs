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
        GameObject go = GameObject.Find("Bush");
        commandRunner.AddCommand(new ApproachCommand(navMeshAgent, go.transform));
    }
}
