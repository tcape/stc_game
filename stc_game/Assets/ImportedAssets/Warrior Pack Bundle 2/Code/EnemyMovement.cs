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
        public float aggroDistance;
        public float stopFollowDistance;
        public float stopDistance = 2f;
        public bool aggroTriggered = false;
        public bool aggro;
        public Vector3 distanceToTarget;
        public GameObject weaponModel;
        public GameObject secondaryWeaponModel;
        float rotationSpeed = 15f;
        public float gravity = -9.83f;
        public float runSpeed = 8f;
        public float runAnimMultiplier = 1f;
        public float walkSpeed = 3f;
        public float strafeSpeed = 3f;
        bool canMove = true;
        //jumping variables
        public float jumpSpeed = 8f;
        bool jumpHold = false;
        [HideInInspector]
        public bool canJump = true;
        float fallingVelocity = -2;
        bool isFalling = false;
        // Used for continuing momentum while in air
        public float inAirSpeed = 8f;
        float maxVelocity = 2f;
        float minVelocity = -2f;
        [HideInInspector]
        public Vector3 newVelocity;
        Vector3 inputVec;
        Vector3 dashInputVec;
        Vector3 targetDirection;
        bool isDashing = false;
        [HideInInspector]
        public bool isGrounded = true;
        [HideInInspector]
        public bool dead = false;
        bool isStrafing;
        [HideInInspector]
        public bool isAiming;
        bool aimingGui;
        [HideInInspector]
        public bool isBlocking = false;
        [HideInInspector]
        public bool isStunned = false;
        [HideInInspector]
        public bool isSitting = false;
        [HideInInspector]
        public bool inBlock;
        [HideInInspector]
        public bool blockGui;
        [HideInInspector]
        public bool weaponSheathed;
        [HideInInspector]
        public bool weaponSheathed2;
        bool isInAir;
        [HideInInspector]
        public bool isStealth;
        public float stealthSpeed;
        [HideInInspector]
        public bool isWall;
        [HideInInspector]
        public bool ledgeGui;
        [HideInInspector]
        public bool ledge;
        public float ledgeSpeed;
        [HideInInspector]
        public int attack = 0;
        bool canChain;
        [HideInInspector]
        public bool specialAttack2Bool;
        private Vector3  startPosition;


        private void Start() // Set fields
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody>();
            target = GameObject.FindGameObjectWithTag("Player");
            startPosition = transform.position;
            aggroTriggered = false;
            aggro = false;
        }

        private void Update()
        {

            UpdateAggro();
            
        }

        private void FixedUpdate()
        {
            UpdateMovement();
            float velocityXel = Math.Abs(rigidBody.velocity.x);
            float velocityZel = Math.Abs(rigidBody.velocity.z);
            animator.SetFloat("Input X", velocityXel);
            animator.SetFloat("Input Z", velocityZel);
        }

        private void LateUpdate() // Update animator values here
        {
            //float velocityXel = Math.Abs(rigidBody.velocity.x);
            //float velocityZel = Math.Abs(rigidBody.velocity.z);
            ////Get local velocity of charcter
            ////float velocityXel = transform.InverseTransformDirection(rigidBody.velocity).x;
            ////float velocityZel = transform.InverseTransformDirection(rigidBody.velocity).z;
            ////Update animator with movement values
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
                //Quaternion targetRotation;
                ////float rotationSpeed = 40f;
                //targetRotation = Quaternion.LookRotation(target.transform.position - new Vector3(transform.position.x, 0, transform.position.z));
                //transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, (rotationSpeed * Time.deltaTime) * rotationSpeed);
                ////rigidBody.velocity.Set(runSpeed, 0, runSpeed);
                //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, runSpeed * Time.deltaTime);

                //move towards the player
                var distance = Math.Abs(Vector3.Distance(transform.position, target.transform.position));
                if (distance > stopDistance)
                {

                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);
                    transform.position += transform.forward * runSpeed * Time.deltaTime;

                    animator.SetBool("Moving", true);
                    animator.SetBool("Running", true);
                }
                else
                {
                    animator.SetBool("Moving", false);
                    animator.SetBool("Running", false);
                }

               
            }
            else if(aggroTriggered && !aggro)
            {
                ReturnToStartPosition();
            }
            else
            {
                animator.SetBool("Moving", false);
                animator.SetBool("Running", false);
            }
        }

        private void ReturnToStartPosition()
        {
            var distance = Math.Abs(Vector3.Distance(transform.position, startPosition));
            if (distance > 0.1f)
            {

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(startPosition - transform.position), rotationSpeed * Time.deltaTime);
                transform.position += transform.forward * runSpeed * Time.deltaTime;

                animator.SetBool("Moving", true);
                animator.SetBool("Running", true);
            }
            else
            {
                aggroTriggered = false;
                animator.SetBool("Moving", false);
                animator.SetBool("Running", false);
            }
        }
    }
}
