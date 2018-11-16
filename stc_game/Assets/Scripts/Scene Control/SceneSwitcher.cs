using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GotoLoginScene()
    {
        SceneManager.LoadScene("LoginUI");
    }

    //Can create simple loads for different scenes
    //Makes this script a kind of master-key to get to scenes
}