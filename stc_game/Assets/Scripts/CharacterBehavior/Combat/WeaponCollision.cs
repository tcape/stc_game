﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{

    public bool hittingTarget;
    public GameObject target;
    

    private void Awake()
    {
        hittingTarget = false;
        target = gameObject.GetComponentInParent<StateController>().target;
    }

    private void Update()
    {
        target = gameObject.GetComponentInParent<StateController>().target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!target)
        {
            return;
        }
        if (other.gameObject.Equals(target))
        {
            hittingTarget = true;
            Debug.Log("Entered Target");
            other.gameObject.GetComponent<CharacterStats>().DoDamage(GetComponentInParent<CharacterStats>());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!target)
        {
            return;
        }

        if (other.gameObject.Equals(target))
        {
            hittingTarget = false;
            Debug.Log("Exited Target");
        }
    }
    
}