using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public Stats stats;
    public GameObject hpBar;
    public GameObject spBar;
    public GameObject goldCounter;
    public GameObject experienceBar;

    public void Awake()
    {
        
    }

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

    public void FindPlayerObject()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>().stats;
    }

    private void UpdateHPBar()
    {
        var scaleValue = (float)(stats.currentHP / stats.maxHP);
        hpBar.transform.localScale = new Vector3(scaleValue, 1f, 1f);
    }

    private void UpdateAPBar()
    {
        var scaleValue = (float)(stats.currentAP / stats.maxAP);
        spBar.transform.localScale = new Vector3(scaleValue, 1f, 1f);
    }

    private void UpdateGoldCount()
    {
        goldCounter.GetComponent<UnityEngine.UI.Text>().text = stats.gold.ToString();
    }

    private void UpdateXPBar()
    {
        var scaleValue = (float)((stats.XP - stats.GetTotalXP()) / (stats.GetNextLevel() - stats.GetTotalXP()));
        experienceBar.transform.localScale = new Vector3(scaleValue, 1f, 1f);
    }
}
