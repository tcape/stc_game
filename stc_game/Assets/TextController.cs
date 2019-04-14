using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    private Stats stats;
    private Camera cam;


    private void Start()
    {
        stats = GetComponentInParent<CharacterStats>().stats;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 v = cam.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(cam.transform.position - v);
        transform.Rotate(0, 180, 0);

        GetComponent<Text>().text = stats.currentHP.ToString();

        if (GetComponent<Text>().text.Equals("0"))
        {
            GetComponent<Text>().text = "";
        }
    }

}
