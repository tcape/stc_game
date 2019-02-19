using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerToDungeon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadSceneAsync("Dungeon1");
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
