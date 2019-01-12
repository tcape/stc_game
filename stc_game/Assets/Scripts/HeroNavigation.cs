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



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = null;
        
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
                if (hit.rigidbody.gameObject.tag.Equals("Enemy"))
                {
                    target = hit.rigidbody.gameObject;
                }

                else
                {
                    target = null;
                }
            }

                
            
        }

        if (!target.Equals(null))
        {
            float speed = agent.velocity.magnitude * speedFactor;
            if (speed < 0.1f)
            {
                //transform.LookAt(target.transform);
                Vector3 deltaVec = target.transform.position - transform.position;
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
        else
        {
            animator.SetBool("Moving", true);
            animator.SetBool("Running", true);
        }
    }
}
