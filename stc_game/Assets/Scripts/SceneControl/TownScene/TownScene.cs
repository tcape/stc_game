using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Devdog.QuestSystemPro.UI;
using Devdog.QuestSystemPro;

public class TownScene : MonoBehaviour
{
    private int num = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneController.Instance.FadeAndLoadScene(GameStrings.Scenes.DungeonScene);
        }
    }


}