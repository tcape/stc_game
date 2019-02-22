using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    private CharacterStats stats;
    private Camera camera;


    private void Start()
    {
        stats = GetComponentInParent<CharacterStats>() ;
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 v = camera.transform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(camera.transform.position - v);
        transform.Rotate(0, 180, 0);

        GetComponent<Text>().text = stats.currentHP.ToString();

        if (GetComponent<Text>().text.Equals("0"))
        {
            GetComponent<Text>().text = "";
        }
    }

}
