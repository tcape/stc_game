using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public double damage;
    public CharacterStats heroStats;

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
                Destroy(gameObject);

                var hit = Instantiate(Resources.Load("FX/FireballHit") as GameObject, other.transform);
                hit.transform.position = other.gameObject.transform.position + new Vector3(0f, 1.5f, 0);
            }
           
        }
    }
}
