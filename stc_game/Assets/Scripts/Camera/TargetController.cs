using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    public Camera cam;
    public GameObject target;
    public GameObject floorTarget;
    public GameObject hero;
    public Vector3 targetOffset;
    public Vector3 floorOffset;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    public float boss1Offset;


    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Target");
    }

    // Update is called once per frame
    void Update()
    {

        // if right-click
        if (Input.GetMouseButton(1))
        {
            // create raycast hit ray from mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // if hit a rigidbody, get the hit
            if (Physics.Raycast(ray, out hit))
            {
                // if hit an Enemy, set target 
                if (hit.rigidbody.gameObject.tag.Equals("Enemy")) // null reference!
                {
                    target = hit.rigidbody.gameObject;
                    transform.position = new Vector3(hit.rigidbody.gameObject.transform.position.x + xOffset,
                                                     hit.rigidbody.gameObject.transform.position.y + yOffset,
                                                     hit.rigidbody.gameObject.transform.position.z + zOffset);
                    floorTarget.transform.position = target.transform.position + floorOffset;
                }
                // if hit a Boss, set target
                else if (hit.rigidbody.gameObject.tag.Equals("Boss1"))
                {
                    target = hit.rigidbody.gameObject;
                    transform.position = new Vector3(hit.rigidbody.gameObject.transform.position.x + xOffset,
                                                     hit.rigidbody.gameObject.transform.position.y + yOffset + boss1Offset,
                                                     hit.rigidbody.gameObject.transform.position.z + zOffset);
                    floorTarget.transform.position = target.transform.position + floorOffset;

                }
                //otherwise set target to self and leave view
                else
                {
                    target = GameObject.FindGameObjectWithTag("Target");
                    transform.position = new Vector3(0, -1000, 0);
                    floorTarget.transform.position = transform.position;

                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            target = GameObject.FindGameObjectWithTag("Target");
            transform.position = new Vector3(0, -1000, 0);
            floorTarget.transform.position = transform.position;
        }
        // Keep transform above target object if no mouse click
        if (target.tag.Equals("Enemy"))
        {
            transform.position = new Vector3(target.transform.position.x + xOffset,
                                             target.transform.position.y + yOffset,
                                             target.transform.position.z + zOffset);
            floorTarget.transform.position = target.transform.position + floorOffset;

        }
        else if (target.tag.Equals("Boss1"))
        {
            transform.position = new Vector3(target.transform.position.x + xOffset,
                                             target.transform.position.y + yOffset + boss1Offset,
                                             target.transform.position.z + zOffset);
            floorTarget.transform.position = target.transform.position + floorOffset;

        }
        else
        {
            transform.position = new Vector3(0, -1000, 0);
            floorTarget.transform.position = transform.position;

        }

        

    }

    private void FixedUpdate()
    {
        if (hero.GetComponent<StateController>().target.Equals(null))
            floorTarget.transform.position = new Vector3(0, -1000, 0);
    }
}
