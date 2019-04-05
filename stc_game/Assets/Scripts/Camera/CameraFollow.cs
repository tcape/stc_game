using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;
    Vector3 offset;
    public float smoothness;
    bool b;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        transform.position = GameObject.FindGameObjectWithTag("CameraData").transform.position;
        transform.rotation = GameObject.FindGameObjectWithTag("CameraData").transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        // mouse scroll wheel zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && GetComponent<Camera>().fieldOfView > 35)
        {
            GetComponent<Camera>().fieldOfView -= 5;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && GetComponent<Camera>().fieldOfView < 60)
        {
            GetComponent<Camera>().fieldOfView += 5;
        }

    }

    private void LateUpdate()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        else
        {
            if (!b)
            {
                offset = transform.position - target.transform.position;
                b = true;
            }

            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * smoothness);
            return;
        }
    }
}
    

