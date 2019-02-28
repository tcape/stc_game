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

    private void OnTriggerEnter(Collider other)
    {
        if (( other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("NPC"))
            && !other.gameObject.GetComponent<CharacterStats>().dead
            && other.isTrigger)
            zoneEnemies.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
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
