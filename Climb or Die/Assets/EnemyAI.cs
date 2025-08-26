using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public float AgentWanderRadius;
    public GameObject player;
    private Rigidbody rb;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        goToPosition(player.transform.position);

        if (Vector3.Distance(player.transform.position, transform.position) > 50)
        {
            Vector3 directionToGo = (player.transform.position - transform.position).normalized;
            rb.AddForce(directionToGo * 700);
        }
    }

    private void goToPosition(Vector3 locaton)
    {
        agent.SetDestination(locaton);
    }
}
