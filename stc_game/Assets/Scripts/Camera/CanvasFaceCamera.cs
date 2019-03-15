using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFaceCamera : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 v = cam.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cam.transform.position - v);
        transform.Rotate(0, 180, 0);
    }
}
