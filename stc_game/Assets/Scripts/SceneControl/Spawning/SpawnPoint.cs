using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public string spawnPointName;

    private void OnValidate()
    {
        spawnPointName = gameObject.name;
    }
}
