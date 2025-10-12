using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float AgentWanderRadius;
    public GameObject player;
    private Rigidbody rb;
    private NavMeshAgent agent;
    public LightingManager lightingManager;

    enum AIState
    {
        Stalking,
        Wandering,
        Following,
        Chasing,
        Searching,
        Waiting
    }

    AIState state;

    void Update ()
    {
        state = SetAIState();
        AI(state);
    }

    AIState SetAIState()
    {
        float randomNum = Random.Range(0f, 100f);

        if (randomNum <= 10f)
        {
            return AIState.Following;
        }
        else if (randomNum <= 20f)
        {
            return AIState.Wandering;
        }
        else if (randomNum <= 40f)
        {
            return AIState.Stalking;
        }
        else
        {
            return AIState.Waiting;
        }
    }

    void AI (AIState aiState)
    {
        if (aiState == AIState.Stalking)
        {
            float stalkingDistance = Random.Range(50f, 100f);

            Stalk(stalkingDistance);
        }
    }

    void Stalk(float distance)
    {
        Vector3 detination = new Vector3(player.transform.position.x - distance, player.transform.position.y, player.transform.position.z);
        agent.SetDestination(detination);
    }
}