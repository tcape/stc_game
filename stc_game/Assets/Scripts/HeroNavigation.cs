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
    public GameObject target;
    public float rotationSpeed;
    public float runThreshold;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
            
        }

        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody.gameObject.tag.Equals("Enemy") || hit.rigidbody.gameObject.tag.Equals("Boss1"))
                {
                    target = hit.rigidbody.gameObject;
                }

                else
                {
                    target = GameObject.FindGameObjectWithTag("Player");
                }
            }
            
        }

        if (target.Equals(GameObject.FindGameObjectWithTag("Player")))
        {
            return;
        }

        else
        {
            float speed = agent.velocity.magnitude * speedFactor;
            if (speed < 0.1f)
            {
                //transform.LookAt(target.transform);
                Vector3 deltaVec = target.transform.position - transform.position;
                if (deltaVec != Vector3.zero)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(deltaVec), Time.deltaTime * rotationSpeed);
            }
            
        }

    }

    private void FixedUpdate()
    {
        float speed = agent.velocity.magnitude * speedFactor;
        animator.SetFloat("Input Z", speed);

        if (speed < 0.1f)
        {
            animator.SetBool("Moving", false);
            animator.SetBool("Running", false);
            
        }
        else if (speed >= 0.1f && speed < runThreshold)
        {
            animator.SetBool("Moving", true);
            animator.SetBool("Running", false);
        }
        else
        {
            animator.SetBool("Moving", true);
            animator.SetBool("Running", true);
        }
    }
}
