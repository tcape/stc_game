using Kryz.CharacterStats.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject[] items;
    public Vector3 offset;
    private bool dropped;
    private GameObject player;
    private HeroClass playerClass;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerClass = player.GetComponent<Hero>().heroClass;
    }

    public void DropItems()
    {
        if (!dropped)
        {
            float rotation = 0;
            foreach (var item in items)
            {
                if (item.GetComponent<GameItem>().item.itemClass == ItemClass.Any ||
                    item.GetComponent<GameItem>().item.itemClass.ToString() == playerClass.ToString())
                {
                    float x = (float)Math.Sin(rotation) * 2f;
                    float z = (float)Math.Cos(rotation) * 2f;
                    var droppedItem = Instantiate(item);
                    droppedItem.transform.position = transform.position + new Vector3(x, 1.5f, z);
                    droppedItem.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0f, 360f), 90);
                    rotation += (float)(Math.PI / 4f);
                }
            }
            dropped = true;
        }
    }
}
