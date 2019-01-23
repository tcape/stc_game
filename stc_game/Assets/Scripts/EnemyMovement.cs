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
        
        [HideInInspector]
        public Animator animator;
        public Warrior warrior;
        private IKHands ikhands;
        Rigidbody rigidBody;
        public GameObject target;

        private Vector3 startPosition;
        public float aggroDistance;
        public float stopFollowDistance;
        public float stopDistance = 2f;
        public bool aggroTriggered = false;
        public bool aggro;
        public Vector3 distanceToTarget;
        public NavMeshAgent agent;
        float rotationSpeed = 15f;

        //public GameObject weaponModel;
        //public GameObject secondaryWeaponModel;

        //public float gravity = -9.83f;
        //public float runSpeed = 8f;
        //public float runAnimMultiplier = 1f;
        //public float walkSpeed = 3f;
        //public float strafeSpeed = 3f;
        //bool canMove = true;
        ////jumping variables
        //public float jumpSpeed = 8f;
        //bool jumpHold = false;
        //[HideInInspector]
        //public bool canJump = true;
        //float fallingVelocity = -2;
        //bool isFalling = false;
        //// Used for continuing momentum while in air
        //public float inAirSpeed = 8f;
        //float maxVelocity = 2f;
        //float minVelocity = -2f;
        //[HideInInspector]
        //public Vector3 newVelocity;
        //Vector3 inputVec;
        //Vector3 dashInputVec;
        //Vector3 targetDirection;
        //bool isDashing = false;
        //[HideInInspector]
        //public bool isGrounded = true;
        //[HideInInspector]
        //public bool dead = false;
        //bool isStrafing;
        //[HideInInspector]
        //public bool isAiming;
        //bool aimingGui;
        //[HideInInspector]
        //public bool isBlocking = false;
        //[HideInInspector]
        //public bool isStunned = false;
        //[HideInInspector]
        //public bool isSitting = false;
        //[HideInInspector]
        //public bool inBlock;
        //[HideInInspector]
        //public bool blockGui;
        //[HideInInspector]
        //public bool weaponSheathed;
        //[HideInInspector]
        //public bool weaponSheathed2;
        //bool isInAir;
        //[HideInInspector]
        //public bool isStealth;
        //public float stealthSpeed;
        //[HideInInspector]
        //public bool isWall;
        //[HideInInspector]
        //public bool ledgeGui;
        //[HideInInspector]
        //public bool ledge;
        //public float ledgeSpeed;
        //[HideInInspector]
        //public int attack = 0;
        //bool canChain;
        [HideInInspector]
        public bool specialAttack2Bool;


        private Vector3 previousPosition;
        


        private void Start() // Set fields
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody>();
            target = GameObject.FindGameObjectWithTag("Player");
            startPosition = transform.position;
            aggroTriggered = false;
            aggro = false;
            animator.SetFloat("Speed", 0f);
            //animator.SetInteger("Attack", 0);

        }

        private void Update()
        {
            //previousPosition = transform.position;
            UpdateAggro();
        }

        private void FixedUpdate()
        {
            UpdateMovement();
            //var speed = (float)Math.Sqrt(rigidBody.velocity.x * rigidBody.velocity.x + rigidBody.velocity.z * rigidBody.velocity.z);
            var speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
            

        }

        private void LateUpdate() // Update animator values here
        {
            //rigidBody.velocity = (previousPosition - transform.position) / Time.deltaTime;


            //float velocityXel = Math.Abs(rigidBody.velocity.x);
            //float velocityZel = Math.Abs(rigidBody.velocity.z);
            //animator.SetFloat("Input X", velocityXel);
            //animator.SetFloat("Input Z", velocityZel);

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
           
        }

        private void UpdateMovement()
        {
            if(aggroTriggered && aggro)
            {
                //move towards the player
                var distance = Math.Abs(Vector3.Distance(transform.position, target.transform.position));
                if (distance > stopDistance)
                {
                    //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);
                    //transform.position += transform.forward * runSpeed * Time.deltaTime;

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

                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(startPosition - transform.position), rotationSpeed * Time.deltaTime);
                //transform.position += transform.forward * runSpeed * Time.deltaTime;

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
