using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Stats stats;
    public GameObject hpBar;
    public GameObject apBar;
    public GameObject goldCounter;
    public GameObject experienceBar;

    public void Update()
    {
        if (stats != null)
        {
            UpdateHPBar();
            UpdateAPBar();
            UpdateGoldCount();
            UpdateXPBar();
        }
    }

    private void OnEnable()
    {
        SceneController.Instance.AfterSceneLoad += FindPlayerObject;
    }

    private void OnDisable()
    {
        SceneController.Instance.AfterSceneLoad -= FindPlayerObject;
    }

    public void FindPlayerObject()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().stats;
    }

    private void UpdateHPBar()
    {
        var scaleValue = (float)(stats.currentHP / stats.maxHP);
        hpBar.GetComponent<Image>().fillAmount = scaleValue;
    }

    private void UpdateAPBar()
    {
        var scaleValue = (float)(stats.currentAP / stats.maxAP);
        apBar.GetComponent<Image>().fillAmount = scaleValue;
    }

    private void UpdateGoldCount()
    {
        goldCounter.GetComponent<Text>().text = stats.gold.ToString();
    }

    private void UpdateXPBar()
    {
        var scaleValue = (float)((stats.XP - stats.GetTotalXP()) / (stats.GetNextLevel() - stats.GetTotalXP()));
        experienceBar.GetComponent<Image>().fillAmount = scaleValue;
    }
}
