using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZoneController : MonoBehaviour
{
    public List<GameObject> zoneEnemies;
    private StateController controller;

    private void Awake()
    {
        zoneEnemies = new List<GameObject>();
        controller = GetComponentInParent<StateController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (( other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("NPC"))
            && !other.gameObject.GetComponent<CharacterStats>().stats.dead
            && other.isTrigger)
            zoneEnemies.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == controller.target)
            controller.target = null;
        zoneEnemies.Remove(other.gameObject);
    }

    private void Update()
    {
        foreach (var enemy in zoneEnemies)
        {
            if (enemy.GetComponent<CharacterStats>().stats.dead)
                zoneEnemies.Remove(enemy);
        }
    }
}
