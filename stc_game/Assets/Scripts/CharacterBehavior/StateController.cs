using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public BehaviorStats stats;
    public State startState;
    public State currentState;
    public State remainState;
    public GameObject aggroSergent;
    public GameObject target;
    public List<Transform> waypointList;
    
    [HideInInspector] public Animator animator;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public Vector3 head;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public int nextWayPoint;

    private bool aiActive;


    private void Start()
    {
        currentState = startState;
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        head = transform.position;
        startPosition = transform.position;
        SetupAI(true, GetComponent<StateController>().waypointList);
        
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
