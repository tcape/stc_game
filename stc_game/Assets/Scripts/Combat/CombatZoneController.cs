using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZoneController : MonoBehaviour
{
    public List<GameObject> zoneEnemies;

    private void Awake()
    {
        zoneEnemies = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.GetComponent<CharacterStats>().dead && other.gameObject.tag.Equals("Enemy")
            || other.gameObject.tag.Equals("NPC"))
            zoneEnemies.Add(other.gameObject);
    }

    private void OnCollisionExit(Collision other)
    {
        zoneEnemies.Remove(other.gameObject);
    }

    private void Update()
    {
        foreach (var enemy in zoneEnemies)
        {
            if (enemy.GetComponent<CharacterStats>().dead)
                zoneEnemies.Remove(enemy);
        }
    }
}
