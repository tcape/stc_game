using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public Button switchScene;

    public void Awake()
    {
        switchScene.onClick.AddListener(GoToLoginScene);
    }

    public void GoToLoginScene()
    {
        SceneManager.LoadScene("TrainingGrounds");
    }

    //Can create simple loads for different scenes
    //Makes this script a kind of master-key to get to scenes
}