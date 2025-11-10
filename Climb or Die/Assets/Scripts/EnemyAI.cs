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

    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
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
        }
    }

    private IEnumerator StalkPlayer()
    {
        float stalkDuration = Random.Range(2f, 5f);
        float elapsed = 0f;
        bool lookedAt = false;

        Vector3 relativeOffset = new Vector3(0, 0, -distance);


        while (stalkTime < stalkingTimer && !lookedAt)
        {
            Vector3 target = player.TransformPoint(relativeOffset);
            agent.SetDestination(target);

            // TODO: Add a check to see if the player is looking at the stalker
            lookedAt = CheckIfPlayerLookingAtMe();

            elapsed += Time.deltaTime;

            yield return null;
        }
    }

    private bool CheckIfPlayerLookingAtMe()
    {
        return false;
    }
}