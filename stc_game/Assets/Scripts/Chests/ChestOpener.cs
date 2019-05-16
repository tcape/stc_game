using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    private Camera cam;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        animator.SetBool("open", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            // raycast at mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if hit something get the hit
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == GetComponent<MeshCollider>() || GetComponentInChildren<MeshCollider>())
                {
                    animator.SetBool("open", true);
                }
            }
        }
    }
}
