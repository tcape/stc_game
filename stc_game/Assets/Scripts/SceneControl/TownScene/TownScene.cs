using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TownScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    private AsyncOperation operation;
    private int num = 0;

    private void Update()
    {
        if (operation != null)
        {
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        num++;

        if (num == 1)
        {
            //
            //loadingScreen.SetActive(true);
            //LoadLevel("Dungeon");
        }
    }


}