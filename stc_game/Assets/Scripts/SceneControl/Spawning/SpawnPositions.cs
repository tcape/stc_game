using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositions : MonoBehaviour
{
    [SerializeField] public SpawnPoint[] spawnPoints;
    private Transform parent;

    private void OnValidate()
    {
        parent = transform;

       if (parent != null)
        {
            spawnPoints = GetComponentsInChildren<SpawnPoint>();
        }
    }
  

    
}
