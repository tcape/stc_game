using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayerTracker : MonoBehaviour
{
    public Stats playerStats;

    private void OnEnable()
    {
        FindPlayerObject();
    }

    public void FindPlayerObject()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().stats;
    }
}
