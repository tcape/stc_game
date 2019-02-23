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
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Town"));
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
            loadingScreen.SetActive(true);
            LoadLevel("Dungeon");
        }
    }

    public void LoadLevel(string scene)
    {
        
       StartCoroutine(LoadAsynchronously(scene));
    }

    IEnumerator LoadAsynchronously(string scene)
    {
        SceneManager.UnloadSceneAsync("Town");
        operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            yield return null;
        }
        
    }
}
