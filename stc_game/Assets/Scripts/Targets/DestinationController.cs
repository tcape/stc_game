﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestinationController : MonoBehaviour
{
    public Camera cam;
    public Hero hero;
    public GameObject target;
    public Vector3 floorOffset;
    private StateController controller;

    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>();
        controller = hero.stateController;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        target = gameObject;
    }

    private void Update()
    {
        UpdateDestination();
    }

    private void UpdateDestination()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
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
                    transform.position = hit.point + floorOffset;
                }
            }
        }

        if (Input.GetMouseButton(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            // raycast at mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if hit something
            if (Physics.Raycast(ray, out hit))
            {
                transform.position = new Vector3(0, -1000, 0);
            }
        }

        if (controller.target != null)
        {
            if (controller.currentState.isAggro)
            {
                transform.position = new Vector3(0, -1000, 0);
            }
            else
                transform.position = new Vector3(target.transform.position.x,
                                             target.transform.position.y,
                                             target.transform.position.z) + floorOffset;
        }

        var distance = Math.Abs(Vector3.Distance(hero.transform.position, transform.position));

        if (distance <= 1)
            transform.position = new Vector3(0, -1000, 0);
    }
}

