using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxCollision : MonoBehaviour
{
    public List<GameObject> touching = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (!GetComponentInParent<CharacterStats>().dead && other.gameObject.tag.Equals("Enemy"))
        {
            touching.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            if (touching.Contains(other.gameObject))
            {
                touching.Remove(other.gameObject);
            }
        }
    }
}
