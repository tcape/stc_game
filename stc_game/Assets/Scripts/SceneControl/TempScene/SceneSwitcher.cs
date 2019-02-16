using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public Button switchScene;
    public string targetScene = "LoginScene";

    public void Awake()
    {
        switchScene.onClick.AddListener(SwitchScene);
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(targetScene);
    }

    //Can create simple loads for different scenes
    //Makes this script a kind of master-key to get to scenes
}