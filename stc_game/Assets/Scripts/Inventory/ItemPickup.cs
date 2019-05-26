using Kryz.CharacterStats.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private Item item;
    private GameObject parent;

    private void Start()
    {
        item = GetComponentInParent<GameItem>().item;
        parent = GetComponentInParent<GameItem>().gameObject;
    }

    public void Pickup()
    {
        var inventory = PersistentScene.Instance.inventory;

        if (inventory.AddItem(item))
        {
            Destroy(parent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            Pickup();
        }
    }
}
