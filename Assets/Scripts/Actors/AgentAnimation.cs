using UnityEngine;
using UnityEngine.AI;

public class AgentAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private NavMeshAgent navMeshAgent;

    [SerializeField]
    private float movementMultiplier = 1.0f;

    private void FixedUpdate()
    {
        float speed = navMeshAgent.velocity.magnitude * movementMultiplier;
        animator.SetFloat("MovementSpeed", speed);
        if (speed > 0)
        {
            Debug.Log(speed);
        }
    }
}
