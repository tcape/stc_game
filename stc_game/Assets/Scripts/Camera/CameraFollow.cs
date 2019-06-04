using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    Vector3 offset;
    public float smoothness;
    public float viewMin;
    public float viewMax;
    bool b;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        var cameraTransform = GameObject.FindGameObjectWithTag("CameraData").transform;
        transform.position = target.transform.position + cameraTransform.position;
        transform.rotation = cameraTransform.rotation;
        GetComponent<Camera>().fieldOfView = 60;
    }

    // Update is called once per frame
    void Update()
    {
        // mouse scroll wheel zoom
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && GetComponent<Camera>().fieldOfView > viewMin)
        {
            GetComponent<Camera>().fieldOfView -= 5;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && GetComponent<Camera>().fieldOfView < viewMax)
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
    

