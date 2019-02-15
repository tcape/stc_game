using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    public Camera cam;
    public GameObject target;
    public GameObject hero;
    public GameObject destination;
    public Vector3 targetOffset;
    public Vector3 floorOffset;


    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Target");
    }

    // Update is called once per frame
    void Update()
    {
        if (hero.GetComponent<StateController>().target == null)
        {
            target = null;
            transform.position = new Vector3(0, -1000, 0); // TODO: set inactive
        }
        else
        {
            // TODO: set active
            target = hero.GetComponent<StateController>().target;
            transform.position = target.transform.position + floorOffset;
            destination.GetComponent<DestinationController>().target = target;
        }

        if (Input.GetMouseButton(0))
        {
            target = GameObject.FindGameObjectWithTag("Target");
            transform.position = new Vector3(0, -1000, 0);
            transform.position = transform.position;
        }

        // Keep transform below target object if no mouse click
        if (target && target.tag.Equals("Enemy") || target.tag.Equals("Boss1"))
        {
            transform.position = target.transform.position + floorOffset;
        }
        else
        {
            transform.position = new Vector3(0, -1000, 0);
        }
    }
}
