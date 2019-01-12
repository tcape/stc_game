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
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis ("Mouse ScrollWheel") > 0 && GetComponent<Camera>().fieldOfView > 20)
        {
            GetComponent<Camera>().fieldOfView--;
        }
        if (Input.GetAxis ("Mouse ScrollWheel") < 0 && GetComponent<Camera>().fieldOfView < 60)
        {
            GetComponent<Camera>().fieldOfView++;
        }
        
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectsWithTag("Player")[0];
            return;
        }
        else
        {
            if(!b)
            {
                offset = transform.position - target.transform.position;
                b = true;
            }

            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * smoothness);
            return;
        }
    }
}
