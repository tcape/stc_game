using Assets.Scripts.CharacterBehavior.Drops;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestOpener : MonoBehaviour
{
    private Camera cam;
    private Animator animator;
    private bool opened;
    public double goldAmount;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        animator.SetBool("open", false);
        opened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OpenAndCloseChest();
        }
    }

    private void OpenAndCloseChest()
    {
        // raycast at mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // if hit something get the hit
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == gameObject.GetComponent<BoxCollider>())
            {
                if (!animator.GetBool("open"))
                {
                    animator.SetBool("open", true);
                    if (!opened)
                    {
                        // instatiate stuff
                        StartCoroutine(SpawnGold());
                        opened = true;
                    }
                }
                else if (animator.GetBool("open"))
                {
                    animator.SetBool("open", false);
                }
            }
        }
    }
    
    private IEnumerator SpawnGold()
    {
        yield return new WaitForSeconds(1f);
        var gold = Instantiate(Resources.Load("Prefabs/Gold1"), gameObject.transform) as GameObject;
        gold.transform.rotation = transform.rotation;
        gold.transform.position = transform.position + new Vector3(0, 0, -1.3f); // need to get this to make gold appear in front of chest everytime
        gold.GetComponent<Gold>().SetAmount(goldAmount);
    }
}
