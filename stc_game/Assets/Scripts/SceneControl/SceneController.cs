using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    public event Action BeforeSceneUnload;
    public event Action AfterSceneLoad;
    public CanvasGroup faderCanvasGroup;
    public string startingSceneName;
    public float fadeDuration = 1f;

    public GameObject loadingScreen;
    public Slider slider;
    private AsyncOperation operation;


    private bool isFading;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        startingSceneName = GameStrings.Scenes.LoginScene;
        faderCanvasGroup.alpha = 1f;
    }

    private void Start()
    {
        //playerSaveData.Save(PlayerMovement.startingPositionKey, initialStartingPositionName);
        StartCoroutine(LoadSceneAndSetActive(startingSceneName));
        StartCoroutine(Fade(0f));
    }

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

    public void LoadBitchasScene(string scene)
    {
        StartCoroutine(LoadSceneAndSetActive(scene));
    }

    public void FadeAndLoadScene(string sceneName)
    {
        if (!isFading)
        {
            FadeAndSwitchScenes(sceneName);
        }
    }

    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        yield return StartCoroutine(Fade(1f));
        if (BeforeSceneUnload != null)
            BeforeSceneUnload();
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
        if (AfterSceneLoad != null)
            AfterSceneLoad();

        yield return StartCoroutine(Fade(0f));
    }

    public IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);
    }

    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        faderCanvasGroup.blocksRaycasts = true;
        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;
        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha,
                fadeSpeed * Time.deltaTime);
            yield return null;
        }
        isFading = false;
        faderCanvasGroup.blocksRaycasts = false;
    }

    // my functions
    public void LoadLevel(string scene)
    {
        LoadAsynchronously(scene);
    }

    IEnumerator LoadAsynchronously(string scene)
    {
        //yield return StartCoroutine(Fade(1f));
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(LoadSceneAndSetActive(scene));
        var operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            yield return null;
        }
        //yield return StartCoroutine(Fade(0f));

    }
}