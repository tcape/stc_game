﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public double damage;
    public CharacterStats heroStats;
    [Space]
    public AudioClip sound;
    [Range(0.0f, 1.0f)]
    public float volume = .5f;
    private GameObject soundObject;
    private AudioSource source;


    private void Start()
    {
        heroStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        damage = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            if (!other.gameObject.GetComponent<CharacterStats>().stats.dead)
            {
                other.gameObject.GetComponent<CharacterStats>().stats.TakeAbilityDamage(heroStats, damage);
                if (!other.gameObject.GetComponent<CharacterStats>().stats.dead)
                    other.gameObject.GetComponent<StateController>().CauseAggro();
                StartCoroutine(PlayAudio());
                Destroy(gameObject);

                var hit = Instantiate(Resources.Load("FX/FireballHit") as GameObject, other.transform);
                hit.transform.position = other.gameObject.transform.position + new Vector3(0f, 1.5f, 0);
            }
           
        }
    }

    private IEnumerator PlayAudio()
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
