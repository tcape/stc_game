using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public BehaviorStats stats;
    public State currentState;
    public State remainState;
    public Animator animator;
    public GameObject target;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public Vector3 head;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    public List<Transform> waypointList;
    [HideInInspector] public int nextWayPoint;

    private bool aiActive;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        head = transform.position + new Vector3(0, 1, 0);
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
        head = transform.position + new Vector3(0, stats.headOffset, 0);
    }


    private void FixedUpdate()
    {
        UpdateAnimator();
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
        if (speed > .1f)
        {
            animator.SetBool("Moving", true);
            if (speed > .4f)
            {
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Running", false);
            }
        }
        else
        {
            animator.SetBool("Moving", false);
            animator.SetBool("Running", false);
        }
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
