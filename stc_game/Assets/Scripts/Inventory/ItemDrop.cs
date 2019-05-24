using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject item;
    public Vector3 offset;
    public Vector3 rotation;
    private bool dropped;

    public void DropItem()
    {
        if (!dropped)
        {
            var droppedItem = Instantiate(item);
            droppedItem.SetActive(false);
            droppedItem.transform.position = transform.position + offset;
            droppedItem.transform.rotation = Quaternion.Euler(rotation);
            droppedItem.SetActive(true);
            dropped = true;
        }
    }
}
