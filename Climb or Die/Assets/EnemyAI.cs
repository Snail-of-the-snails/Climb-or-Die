using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;

    public float AgentWanderRadius;

    public GameObject player;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector2 point;
        if (Vector3.Distance(player.transform.position, transform.position) < AgentWanderRadius)
        {
            point = Random.insideUnitSphere* AgentWanderRadius;
        }
        else
        {
            point = new Vector2(player.transform.position.x, player.transform.position.z);
        }

        goToPosition(new Vector3(point.x, transform.position.y, point.y));
    }

    private void goToPosition(Vector3 locaton)
    {
        agent.SetDestination(locaton);
    }
}
