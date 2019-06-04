using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetController : MonoBehaviour
{
    public Camera cam;
    public GameObject target;
    public GameObject hero;
    public GameObject destination;
    public Vector3 targetOffset;
    public Vector3 floorOffset;
    public CombatZoneController zone;
    public StateController controller;
    private int currentTarget = -1;

    private void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Target");
        destination = GameObject.FindGameObjectWithTag("Destination");
        zone = hero.GetComponentInChildren<CombatZoneController>();
        controller = hero.GetComponent<StateController>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AdvanceTarget();
            controller.target = zone.zoneEnemies[currentTarget];
            target.SetActive(true);
        }

        if (controller.target == null)
        {
            target = GameObject.FindGameObjectWithTag("Target");
            transform.position = new Vector3(0, -1000, 0); // TODO: set inactive
        }
        else
        {
            // TODO: set active
            target = hero.GetComponent<StateController>().target;
            transform.position = target.transform.position + floorOffset;
            target.SetActive(true);
            if (!hero.GetComponent<StateController>().currentState.isAggro)
                destination.GetComponent<DestinationController>().target = target;
        }

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {

            target.SetActive(true);
            transform.position = transform.position;
        }

        // Keep transform below target object if no mouse click
        if (target && target.tag.Equals("Enemy") || target.tag.Equals("NPC"))
        {
            target.SetActive(true);
            transform.position = target.transform.position + floorOffset;
        }
        else
        {
            transform.position = new Vector3(0, -1000, 0);
        }
    }

    private void AdvanceTarget()
    {
        currentTarget++;
        if (currentTarget >= zone.zoneEnemies.Count)
            currentTarget = 0;
    }
}
