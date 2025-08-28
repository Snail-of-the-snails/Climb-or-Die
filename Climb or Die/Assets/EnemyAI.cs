using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float AgentWanderRadius;
    public GameObject player;
    private Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.linearDamping = 1f;
        rb.angularDamping = 0.5f;
    }

    void Update()
    {
        Vector3 playerLocation = SetDestination();
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer > 50f)
        {
            rb.AddForce(playerLocation * 1000, ForceMode.Force);
            rb.linearDamping = 0f;
            rb.angularDamping = 0f;
        }
        else
        {
            rb.AddForce(playerLocation * 2f, ForceMode.Force);
        }
    }

    private Vector3 SetDestination()
    {
        return (player.transform.position - transform.position).normalized;
    }
}