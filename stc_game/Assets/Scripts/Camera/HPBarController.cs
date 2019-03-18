﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    private CharacterStats stats;
    private Camera cam;
    private Image image;

    private void Start()
    {
        stats = GetComponentInParent<CharacterStats>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        //Vector3 v = cam.transform.position - transform.position;
        //v.x = v.z = 0.0f;
        //transform.LookAt(cam.transform.position - v);
        //transform.Rotate(0, 180, 0);

        image.fillAmount = (float) (stats.currentHP / stats.maxHP);

    }
}