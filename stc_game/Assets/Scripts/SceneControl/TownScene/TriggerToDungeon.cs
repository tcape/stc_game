using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerToDungeon : MonoBehaviour
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
                //operation.allowSceneActivation = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        num++;

        if (num == 1)
        {
            loadingScreen.SetActive(true);
            LoadLevel("Dungeon1");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    public void LoadLevel(string scene)
    {
        
       StartCoroutine(LoadAsynchronously(scene));
    }

    IEnumerator LoadAsynchronously(string scene)
    {
        operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            yield return null;
        }
        
    }
}
