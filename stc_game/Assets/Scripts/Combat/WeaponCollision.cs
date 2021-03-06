﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    public AudioClip sound = null;
    [Range(0.0f, 1.0f)]
    public float volume;
    private GameObject soundObject;
    private AudioSource source;

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
        if (!target || !other.isTrigger)
        {
            return;
        }
        if (other.gameObject.Equals(target))
        {
            hittingTarget = true;
            //Debug.Log("Entered Target");
            if (!GetComponentInParent<CharacterStats>().stats.dead && (other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Player")))
            {
                if(sound != null)
                    StartCoroutine(PlayHitAudio());

                other.gameObject.GetComponent<CharacterStats>().stats.TakeMeleeDamage(GetComponentInParent<CharacterStats>());
            }
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
            //Debug.Log("Exited Target");
        }
    }

    private IEnumerator PlayHitAudio()
    {
        soundObject = new GameObject("instancedSoundObject");
        soundObject.AddComponent<AudioSource>();
        source = soundObject.GetComponent<AudioSource>();
        source.clip = sound;
        source.volume = volume;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        Destroy(soundObject);
    }
}
