using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationController : MonoBehaviour
{
    public Camera cam;
    public GameObject hero;
    public GameObject target;
    public Vector3 floorOffset;


    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        target = gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // raycast at mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if hit something
            if (Physics.Raycast(ray, out hit))
            {
                // if hit enemy, put the destination target on the floor rather than on body
                if (hit.collider.gameObject.tag.Equals("Enemy") || hit.collider.gameObject.tag.Equals("NPC"))
                {
                    target = hit.collider.gameObject;
                }
                else
                {
                    target = null;
                    transform.position = hit.point;
                }
            }
        }

        if (Input.GetMouseButton(1))
        {
            // raycast at mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if hit something
            if (Physics.Raycast(ray, out hit))
            {
                // if hit enemy, put the destination target on the floor rather than on body
                //if (hit.rigidbody.gameObject.tag.Equals("Enemy") || hit.rigidbody.gameObject.tag.Equals("Boss1"))
                {
                    transform.position = new Vector3(0, -1000, 0);
                }
            }
        }

        if (hero.GetComponent<StateController>().target != null)
        {
            if (hero.GetComponent<StateController>().currentState.isAggro)
            {
                transform.position = new Vector3(0, -1000, 0);
            }
            else
                transform.position = new Vector3(target.transform.position.x,
                                             target.transform.position.y,
                                             target.transform.position.z) + floorOffset;
        }

        var distance = Math.Abs(Vector3.Distance(hero.transform.position, transform.position));

        if (distance < hero.GetComponent<StateController>().stats.stoppingDistance)
            transform.position = new Vector3(0, -1000, 0);
    }
}
