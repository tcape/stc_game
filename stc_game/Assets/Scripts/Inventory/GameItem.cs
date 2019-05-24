using Kryz.CharacterStats.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public Item item;
    public GameObject prefab;

    public void Pickup()
    {
        var inventory = PersistentScene.Instance.inventory;
            
        if(inventory.AddItem(item))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Pickup();
        }
    }
}
