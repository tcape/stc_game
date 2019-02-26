using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TownScene : MonoBehaviour
{
    private int num = 0;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        num++;

        if (num == 1)
        {
            SceneController.Instance.LoadBitchasScene(GameStrings.Scenes.DungeonScene);
            //
            //loadingScreen.SetActive(true);
            //LoadLevel("Dungeon");
        }
    }


}