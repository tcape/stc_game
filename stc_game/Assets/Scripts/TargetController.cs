using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    public Camera cam;
    public GameObject target;
    public GameObject hero;
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
                // if hit an Enemy, set target to object hit and move transform above object
                if (hit.rigidbody.gameObject.tag.Equals("Enemy"))
                {
                    target = hit.rigidbody.gameObject;
                    transform.position = new Vector3(hit.rigidbody.gameObject.transform.position.x + xOffset,
                                                     hit.rigidbody.gameObject.transform.position.y + yOffset,
                                                     hit.rigidbody.gameObject.transform.position.z + zOffset);
                }
                // if hit a Boss, set target and move transform above object
                else if (hit.rigidbody.gameObject.tag.Equals("Boss1"))
                {
                    target = hit.rigidbody.gameObject;
                    transform.position = new Vector3(hit.rigidbody.gameObject.transform.position.x + xOffset,
                                                     hit.rigidbody.gameObject.transform.position.y + yOffset + boss1Offset,
                                                     hit.rigidbody.gameObject.transform.position.z + zOffset);
                }
                //otherwise set target to self and leave view
                else
                {
                    target = GameObject.FindGameObjectWithTag("Target");
                    transform.position = new Vector3(0,-1000,0);
                }
            }
        }

        // Keep transform above target object if no mouse click
        if (target.tag.Equals("Enemy"))
        {
            transform.position = new Vector3(target.transform.position.x + xOffset,
                                             target.transform.position.y + yOffset,
                                             target.transform.position.z + zOffset);
        }
        else if (target.tag.Equals("Boss1"))
        {
            transform.position = new Vector3(target.transform.position.x + xOffset,
                                             target.transform.position.y + yOffset + boss1Offset,
                                             target.transform.position.z + zOffset);
            
        }
        else
        {
            transform.position = new Vector3(0, -1000, 0);
        }
    }
}
