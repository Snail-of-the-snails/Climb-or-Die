using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float AgentWanderRadius;
    public GameObject player;
    private Rigidbody rb;
    private NavMeshAgent agent;
    public LightingManager lightingManager;
    bool isRunning = false;

    [Header("Agent Settings")]
    [Range(1f, 3f)] public float chaseSpeedMultiplier = 1.25f;
    [Range(3.5f, 4.5f)] public float agentSpeed = 3.5f;

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
        agent.speed = agentSpeed;
    }

    AIState state;

    void Update ()
    {
        if (!isRunning)
        {
            state = AIState.Stalking; //SetAIState();
            StartCoroutine(AI(state));
        }
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

    IEnumerator AI (AIState aiState)
    {
        agent.speed = agentSpeed;
        isRunning = true;
        float duration = Random.Range(20f, 50f);

        if (aiState == AIState.Stalking)
        {
            float stalkingDistance = Random.Range(50f, 100f);
            yield return StartCoroutine(Stalk(stalkingDistance, duration));
        }

        isRunning = false;
    }

    private IEnumerator Stalk(float distance, float stalkDuration)
    {
        float elapsed = 0f;
        bool lookedAt = false;

        Vector3 relativeOffset = new Vector3(0, 0, -distance);
        transform.position = player.transform.position + relativeOffset;

        agent.speed = agentSpeed;

        while (elapsed < stalkDuration && !lookedAt)
        {
            Vector3 target = player.transform.position + relativeOffset;
            agent.SetDestination(target);

            lookedAt = CheckIfPlayerLookingAtMe(distance / 4);

            elapsed += Time.deltaTime;

            yield return null;
        }

        if (lookedAt && lightingManager.isNight)
        {
            yield return StartCoroutine(Chase());
        }
        else
        {
            yield return StartCoroutine(Flee());
        }
    }

    private bool CheckIfPlayerLookingAtMe(float detectionRange)
    {
        Vector3 playerPos = player.transform.position;
        Vector3 stalkerPos = transform.position;

        Vector3 playerForward = player.transform.forward;

        Vector3 directionToStalker = (stalkerPos - playerPos).normalized;

        float dot = Vector3.Dot(playerForward, directionToStalker);
        float fovThreshold = 0.97f;

        float distance = Vector3.Distance(playerPos, stalkerPos);


        if (dot > fovThreshold && distance < detectionRange)
        {
            if (Physics.Raycast(playerPos + Vector3.up * 1.6f, directionToStalker, out RaycastHit hit, detectionRange))
            {
                if (hit.transform == transform)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private IEnumerator Flee()
    {
        float elapsed = 0f;
        float fleeDuration = Random.Range(15f, 35f);
        agent.speed = agentSpeed * chaseSpeedMultiplier;
        agent.SetDestination(transform.forward * -1 * 100);

        while (elapsed < fleeDuration)
        {
            agent.SetDestination(transform.forward * 100);

            elapsed += Time.deltaTime;

            yield return null;
        }
    }
    
    private IEnumerator Chase()
    {
        Debug.Log("Chasing");
        float elapsed = 0f;
        float chaseDuration = Random.Range(50f, 90f);
        agent.speed = agentSpeed * chaseSpeedMultiplier;

        while (elapsed < chaseDuration)
        {
            agent.SetDestination(player.transform.position);

            elapsed += Time.deltaTime;

            yield return null;
        }

        yield return StartCoroutine(Flee());
    }
}