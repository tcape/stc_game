using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0,1,0), 10.0f * Time.deltaTime);
    }
}
