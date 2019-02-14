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


        if (hero.GetComponent<StateController>().target == null)
        {
            target = null;
            floorTarget.transform.position = new Vector3(0, -1000, 0);
        }

        else
        {
            target = hero.GetComponent<StateController>().target;

            transform.position = new Vector3(target.transform.position.x,
                                             target.transform.position.y,
                                             target.transform.position.z);

            floorTarget.transform.position = target.transform.position + floorOffset;
        }

        if (Input.GetMouseButton(0))
        {
            target = GameObject.FindGameObjectWithTag("Target");
            transform.position = new Vector3(0, -1000, 0);
            floorTarget.transform.position = transform.position;
        }


        // Keep transform below target object if no mouse click
        if (target && target.tag.Equals("Enemy") || target.tag.Equals("Boss1"))
        {
            transform.position = new Vector3(target.transform.position.x + xOffset,
                                             target.transform.position.y + yOffset,
                                             target.transform.position.z + zOffset);
            floorTarget.transform.position = target.transform.position + floorOffset;

        }
        else
        {
            transform.position = new Vector3(0, -1000, 0);
            floorTarget.transform.position = transform.position;

        }
        
    }

}
