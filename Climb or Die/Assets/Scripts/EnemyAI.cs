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
    public GameObject entityObj;
    private BoxCollider collider;
    private bool paused = false;
    private bool gunshotFlee = false;

    [Header("Agent Settings")]
    [Range(1f, 3f)] public float chaseSpeedMultiplier = 1.25f;
    [Range(3.5f, 4.5f)] public float agentSpeed = 3.5f;
    public ChaseVignette chaseVignette;

    [Header("Music Settings")]
    public PlayMusic gameMusic;
    public ChaseMusic chaseMusic;
    public SpawnSounds spawnSounds;

    [Header("Agent Animation")]
    public Animator animator;

    enum AIState
    {
        Stalking,
        Wandering,
        Following,
        Chasing,
        Waiting,
    }

    enum MovementState
    {
        Idle,
        Walking,
        Running
    }

    void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();
        collider = transform.GetComponent<BoxCollider>();
        agent.speed = agentSpeed;
        state = AIState.Waiting;
    }

    void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    AIState state;
    MovementState movementState;

    void Update ()
    {
        if (!isRunning)
        {
            if (!paused)
            {
                state = SetAIState();
                StartCoroutine(AI(state));
            }
        }

        CheckToAnimate();
    }

    AIState SetAIState()
    {
        float randomNum = Random.Range(0f, 100f);

        if (!lightingManager.isNight)
        {
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
        else
        {
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
            else if (randomNum <= 50f)
            {
                return AIState.Chasing;
            }
            else
            {
                return AIState.Waiting;
            }
        }
    }

    IEnumerator AI (AIState aiState)
    {
        collider.enabled = true;
        entityObj.SetActive(true);
        movementState = MovementState.Idle;

        Debug.Log(state);

        if (state != AIState.Waiting)
        {
            spawnSounds.PlaySpawnSound();
        }

        agent.speed = agentSpeed;
        isRunning = true;
        float duration = Random.Range(20f, 50f);

        if (aiState == AIState.Stalking)
        {
            float stalkingDistance = Random.Range(25f, 50f);
            yield return StartCoroutine(Stalk(stalkingDistance, duration));
        }
        else if (aiState == AIState.Wandering)
        {
            float wanderRadius = Random.Range(50f, 75f);
            yield return StartCoroutine(Wandering(wanderRadius, duration));
        }
        else if (aiState == AIState.Following)
        {
            // Following behavior can be implemented here
        }
        else if (aiState == AIState.Waiting)
        {
            collider.enabled = false;
            entityObj.SetActive(false);
            yield return new WaitForSeconds(duration / 2);
        }
        else if (aiState == AIState.Chasing)
        {
            yield return StartCoroutine(BeginChase());
        }

        isRunning = false;
    }


    // AI Behaviors
    private IEnumerator Stalk(float distance, float stalkDuration)
    {
        float elapsed = 0f;
        bool lookedAt = false;

        Vector3 relativeOffset = -player.transform.forward * distance;
        transform.position = player.transform.position + relativeOffset;

        agent.speed = agentSpeed;



        while (elapsed < stalkDuration && !lookedAt && !gunshotFlee)
        {
            Vector3 target = player.transform.position + relativeOffset;
            agent.SetDestination(target);

            lookedAt = CheckIfPlayerLookingAtMe(distance / 4);

            yield return StartCoroutine(CheckIfPaused());

            elapsed += Time.deltaTime;

            yield return null;

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                movementState = MovementState.Idle;
            }
            else
            {
                movementState = MovementState.Walking;
            }
        }

        if (lookedAt && lightingManager.isNight && !gunshotFlee)
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
        agent.speed = agentSpeed * chaseSpeedMultiplier;

        movementState = MovementState.Running;

        while (Vector3.Distance(transform.position, player.transform.position) < 150f)
        {
            Vector3 fleeDir = (transform.position - player.transform.position).normalized;
            Vector3 fleeTarget = transform.position + fleeDir * 20f;

            yield return StartCoroutine(CheckIfPaused());

            agent.SetDestination(fleeTarget);
            yield return null;
        }
    }
    
    private IEnumerator Chase()
    {
        if (state != AIState.Chasing)
        {
            state = AIState.Chasing;
        }

        movementState = MovementState.Running;

        StartCoroutine(chaseVignette.FadeScreenIn());

        float elapsed = 0f;
        float chaseDuration = Random.Range(30f, 50f);
        agent.speed = agentSpeed * chaseSpeedMultiplier;
        StartCoroutine(gameMusic.StopMusic());
        StartCoroutine(chaseMusic.Play());

        while (elapsed < chaseDuration && !gunshotFlee)
        {
            agent.SetDestination(player.transform.position);

            elapsed += Time.deltaTime;

            yield return StartCoroutine(CheckIfPaused());

            yield return null;
        }

        StartCoroutine(chaseVignette.FadeScreenOut());
        chaseMusic.StopPlaying();
        gameMusic.StartMusic();
        yield return StartCoroutine(Flee());
    }

    private IEnumerator Wandering(float radius, float duration)
    {
        float elapsed = 0f;
        Vector3 center = player.transform.forward * radius + player.transform.position;
        transform.position = center;
        bool closeToPlayer = false;

        while (elapsed < duration)
        {
            Vector3 randomPoint;

            if (Vector3.Distance(transform.position, agent.destination) < 5f && !gunshotFlee) {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(center + Random.insideUnitSphere * radius, out hit, radius, NavMesh.AllAreas))
                {
                    randomPoint = hit.position;
                    agent.SetDestination(randomPoint);
                }
                else
                {
                    yield return null;
                    continue;
                }
            }

            if (Vector3.Distance(transform.position, player.transform.position) < 20f)
            {
                closeToPlayer = true;
                break;
            }

            elapsed += Time.deltaTime;

            yield return StartCoroutine(CheckIfPaused());

            center = transform.position;

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                movementState = MovementState.Idle;
            }
            else
            {
                movementState = MovementState.Walking;
            }
        }

        if (closeToPlayer && lightingManager.isNight && !gunshotFlee)
        {
            yield return StartCoroutine(Chase());
        }
        else
        {
            yield return StartCoroutine(Flee());
        }
    }

    private IEnumerator BeginChase()
    {
        Vector3 offset = -player.transform.forward * 75;
        transform.position = player.transform.position + offset;

        yield return StartCoroutine(Chase());
    }

    // Paused State Handling

    private IEnumerator CheckIfPaused()
    {
        MovementState prevState = movementState;
        Vector3 desition = agent.destination;
        if (paused)
        {
            agent.destination = transform.position;
            yield return new WaitUntil(() => !paused);
            movementState = MovementState.Idle;
        }

        movementState = prevState;
        agent.destination = desition;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        paused = !(newGameState == GameState.Gameplay);
    }

    public void FleeFromGunshot()
    {
        gunshotFlee = true;
    }

    // Animation

    private void CheckToAnimate()
    {
        if (movementState == MovementState.Idle)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walking", false);
        }
        else if (movementState == MovementState.Walking || movementState == MovementState.Running)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", true);
        }
    }
}