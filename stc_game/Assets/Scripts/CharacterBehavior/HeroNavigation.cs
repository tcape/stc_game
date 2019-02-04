using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroNavigation : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public Animator animator;
    public Rigidbody rb;
    public float speedFactor;
    public float attackDistance;
    public GameObject target;
    public GameObject destination;
    public float rotationSpeed;
    public float runThreshold;
    public Vector3 offset;
    public float yTargetPosition;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // if left-click
        if (Input.GetMouseButton(0))
        {
            // raycast at mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if hit something
            if(Physics.Raycast(ray, out hit))
            {
                // set destination of nav mesh agent
                agent.SetDestination(hit.point);
                
                // if hit enemy or player, put the destination target on the floor rather than on body
                if (hit.rigidbody.gameObject.tag.Equals("Enemy") || hit.rigidbody.gameObject.tag.Equals("Boss1") || hit.rigidbody.gameObject.tag.Equals("Player"))
                {
                    destination.transform.position = new Vector3(hit.point.x, yTargetPosition, hit.point.z);
                }
                else
                {
                    destination.transform.position = hit.point + offset;
                    target = GameObject.FindGameObjectWithTag("Player");
                }
            }
        }

        // if right-click
        if (Input.GetMouseButton(1))
        {
            // raycast at mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if hit something get the hit
            if (Physics.Raycast(ray, out hit))
            {
                // if hit enemy or boss, set hero's target
                if (hit.rigidbody.gameObject.tag.Equals("Enemy") || hit.rigidbody.gameObject.tag.Equals("Boss1"))
                {
                    target = hit.rigidbody.gameObject;
                    destination.transform.position = new Vector3(0, -1000, 0);
                    var distance = Math.Abs(Vector3.Distance(transform.position, target.transform.position));

                    if (distance > attackDistance)
                        agent.SetDestination(target.transform.position);
                }
                // otherwise, set hero's target to self
                else
                {
                    target = GameObject.FindGameObjectWithTag("Player");
                }
            }
            
        }
        // if target is self, return
        if (target.Equals(GameObject.FindGameObjectWithTag("Player")))
        {
            agent.stoppingDistance = 1f;
            return;
        }
        // otherwise, check speed
        else
        {
            float speed = agent.velocity.magnitude;
            // if speed is very slow or stopped, look at the target
            if (speed < 0.1f)
            {
                Vector3 deltaVec = target.transform.position - transform.position;
                if (deltaVec != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(deltaVec), Time.deltaTime * rotationSpeed);
                }
              
               
            }
            
        }
        if (target.gameObject.tag.Equals("Enemy") || target.gameObject.tag.Equals("Boss1"))
        {
            agent.stoppingDistance = 3f;
            var distance = Math.Abs(Vector3.Distance(transform.position, target.transform.position));

            if (distance > attackDistance)
                agent.SetDestination(target.transform.position);
        }
      


    }

    private void FixedUpdate()
    {
        // get speed and set animator values
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Input Z", speed);


        if (speed < runThreshold)
        {
            animator.SetBool("Moving", false);
            animator.SetBool("Running", false);
            destination.transform.position = new Vector3(0, -1000, 0);


            if (target.Equals(GameObject.FindGameObjectWithTag("Player")))
            {
                return;
            }

            var distance = Math.Abs(Vector3.Distance(transform.position, target.transform.position));

            if (distance <= 4f)
            {
                if (animator.GetInteger("Attack").Equals(0))
                {
                    animator.SetInteger("Attack", 1);
                    return;
                }

                if (animator.GetInteger("Attack").Equals(1))
                {
                    animator.SetInteger("Attack", 2);
                    return;

                }
                if (animator.GetInteger("Attack").Equals(2))
                {
                    animator.SetInteger("Attack", 1);
                    return;
                }
            }
        }
        else if (speed >= 0.1f && speed < runThreshold)
        {
            animator.SetBool("Moving", true);
            animator.SetBool("Running", true);
            var distance = Math.Abs(Vector3.Distance(transform.position, target.transform.position));

            if (distance < attackDistance)
            {
                if (animator.GetInteger("Attack").Equals(0))
                {
                    animator.SetInteger("Attack", 1);
                    return;
                }

                if (animator.GetInteger("Attack").Equals(1))
                {
                    animator.SetInteger("Attack", 2);
                    return;

                }
                if (animator.GetInteger("Attack").Equals(2))
                {
                    animator.SetInteger("Attack", 1);
                    return;
                }
            }

        }
        else
        {
            animator.SetBool("Moving", true);
            animator.SetBool("Running", true);
            animator.SetInteger("Attack", 0);

        }
    }
}
