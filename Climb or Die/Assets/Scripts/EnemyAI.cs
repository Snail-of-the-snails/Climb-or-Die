using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float AgentWanderRadius;
    public GameObject player;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private bool setDestination = false;
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

    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        setDestination = false;
    }

    AIState state;

    void Update ()
    {
        state = AIState.Stalking; //SetAIState();
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

            if (!setDestination)
            {
                setDestination = true;
            }
        }
    }

    void Stalk(float distance)
    {
        Vector3 target;
        Vector3 reletiveOffset;

        if (!setDestination) {
            reletiveOffset = new Vector3(0, 0, 0 - distance);
            target = player.transform.TransformPoint(reletiveOffset);
        }
        else
        {
            reletiveOffset = player.transform.InverseTransformPoint(agent.destination);
            target = player.transform.position - reletiveOffset;
        }

        agent.SetDestination(target);
    }
}