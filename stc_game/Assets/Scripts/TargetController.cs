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


    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody.gameObject.tag.Equals("Enemy"))
                {
                    target = hit.rigidbody.gameObject;
                    transform.position = new Vector3(hit.rigidbody.gameObject.transform.position.x + xOffset,
                                                     hit.rigidbody.gameObject.transform.position.y + yOffset,
                                                     hit.rigidbody.gameObject.transform.position.z + zOffset);
                }
                else
                {
                    target = null;
                    transform.position = new Vector3(0,-1000,0);
                }
            }
        }

        if (!target.Equals(null))
        {
            transform.position = new Vector3(target.transform.position.x + xOffset,
                                             target.transform.position.y + yOffset,
                                             target.transform.position.z + zOffset);
        }
        else
        {
            transform.position = new Vector3(0, -1000, 0);
        }
    }
}
