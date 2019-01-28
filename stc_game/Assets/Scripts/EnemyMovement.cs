using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace Assets.ImportedAssets.Warrior_Pack_Bundle_2.Code
{
    class EnemyMovement : MonoBehaviour
    {
        public Warrior warrior;
        public GameObject squadSergent;
        public float aggroDistance;
        public float stopFollowDistance;
        public float stopDistance = 2f;
        public float rotationSpeed = 15f;

        [HideInInspector] public Animator animator;
        [HideInInspector] Rigidbody rigidBody;
        [HideInInspector] public GameObject target;
        [HideInInspector] public bool aggroTriggered = false;
        [HideInInspector] public bool aggro;
        [HideInInspector] public Vector3 distanceToTarget;
        [HideInInspector] public NavMeshAgent agent;
        [HideInInspector] public bool specialAttack2Bool;

        private Vector3 startPosition;
        private Vector3 previousPosition;
        


        private void Start() // Set fields
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            target = GameObject.FindGameObjectWithTag("Player");
            startPosition = transform.position;
            aggroTriggered = false;
            aggro = false;
            animator.SetFloat("Speed", 0f);
        }

        private void Update()
        {
            UpdateAggro();
        }

        private void FixedUpdate()
        {
            UpdateMovement();
            var speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }

        private void LateUpdate() // Update animator values here, or not?
        {

        }

        private void UpdateAggro()
        {
           
            float xDistance = target.transform.position.x - transform.position.x;
            float zDistance = target.transform.position.z - transform.position.z;

            distanceToTarget.x = Math.Abs(xDistance);
            distanceToTarget.y = 0;
            distanceToTarget.z = Math.Abs(zDistance);

            if (aggro)
            {
                if (distanceToTarget.magnitude > stopFollowDistance)
                {
                    aggro = false;
                }
            }

            if (distanceToTarget.magnitude < aggroDistance) 
            {
                aggro = true;
                aggroTriggered = true;
            }

            if (squadSergent.Equals(null))
            {
                return;
            }

            if (squadSergent.GetComponent<EnemyMovement>().aggro)
            {
                aggroTriggered = true;
                aggro = true;
            }

           
        }

        private void UpdateMovement()
        {
            if(aggroTriggered && aggro)
            {
                //move towards the player
                var distance = Math.Abs(Vector3.Distance(transform.position, target.transform.position));
                if (distance > stopDistance)
                {
                    agent.SetDestination(target.transform.position);
                    
                    animator.SetBool("Moving", true);
                    animator.SetBool("Running", true);
                }
                else // stop and attack
                {

                    animator.SetBool("Moving", false);
                    animator.SetBool("Running", false);

                    //if (animator.GetInteger("Attack").Equals(0))
                    //{
                    //    animator.SetInteger("Attack", 1);
                    //    return;
                    //}

                    //if (animator.GetInteger("Attack").Equals(1))
                    //{
                    //    animator.SetInteger("Attack", 2);
                    //    return;

                    //}
                    //if (animator.GetInteger("Attack").Equals(2))
                    //{
                    //    animator.SetInteger("Attack", 1);
                    //    return;
                    //}
                }
                Vector3 deltaVec = target.transform.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(deltaVec), Time.deltaTime * rotationSpeed);
            }
            else if(aggroTriggered && !aggro)
            {
                animator.SetInteger("Attack", 0);
                ReturnToStartPosition(stopDistance);

            }
            else
            {
                animator.SetInteger("Attack", 0);
                animator.SetBool("Moving", false);
                animator.SetBool("Running", false);
            }
        }

        private void ReturnToStartPosition(float defaultStoppingDistance)
        {
            var distance = Math.Abs(Vector3.Distance(transform.position, startPosition));
            agent.stoppingDistance = 0.5f;
            if (distance > agent.stoppingDistance)
            {
                agent.SetDestination(startPosition);
                animator.SetBool("Moving", true);
                animator.SetBool("Running", true);
            }
            else
            {
                agent.stoppingDistance = defaultStoppingDistance;
                aggroTriggered = false;
                animator.SetBool("Moving", false);
                animator.SetBool("Running", false);
            }
        }
    }
}
