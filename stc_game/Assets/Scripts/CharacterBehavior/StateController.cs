using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public BehaviorStats stats;
    public State startState;
    public State currentState;
    public State remainState;
    public State aggroState;
    public GameObject aggroSergent;
    public GameObject target;
    public Camera cam;
    public GameObject destination;
    public List<Transform> waypointList;
    public SaveData playerSaveData;
    public const string startingPositionKey = "startingPosition";
    public string startingPositionName = "";
    [HideInInspector] public CharacterStats characterStats;
    [HideInInspector] public Animator animator;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public Vector3 head;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Quaternion startRotation;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public int nextWayPoint;

    private bool aiActive;


    private void Start()
    {
        currentState = startState;
        if (gameObject.tag.Equals("Player"))
        {
            target = null;
        }
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        destination = GameObject.FindGameObjectWithTag("Destination");
        head = transform.position;
        startPosition = transform.position;
        startRotation = transform.rotation;
        SetupAI(true, GetComponent<StateController>().waypointList);
        characterStats = GetComponent<CharacterStats>();

    }

    public void SetupAI(bool aiActivationFromCharacter, List<Transform> waypointsFromCharacter)
    {
        waypointList = waypointsFromCharacter;
        aiActive = aiActivationFromCharacter;

        if (aiActive)
        {
            navMeshAgent.enabled = true;
        }
        else
        {
            navMeshAgent.enabled = false;
        }
    }

    private void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);

      }


    private void FixedUpdate()
    {
        UpdateAnimator();
        head = transform.position + new Vector3(0, stats.headOffset, 0);
    }

    private void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(head, stats.headGizmoRadius);
        }
    }

    public void UpdateAnimator()
    {
        var speed = navMeshAgent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        animator.SetBool("Moving", speed > .1);
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }
}
