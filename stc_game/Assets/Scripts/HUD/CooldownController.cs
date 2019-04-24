using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownController : MonoBehaviour
{
    public Transform abilitySlot;
    private Image cooldown;
    private Ability ability;
    private float startTime;
    private AbilityManager abilityManager;

    private void Awake()
    {
        cooldown = GetComponent<Image>();
        cooldown.fillAmount = 0;
        startTime = 0;
    }

    private void OnGUI()
    {
        ability = abilitySlot.GetComponent<AbilitySlot>().ability;

    }

    private void Update()
    {
        if (ability)  
        {
            if (ability.startTime < Time.time)
                cooldown.fillAmount = 1f - (Time.time - ability.startTime) / ability.cooldown;
        }
    }


    private void OnEnable()
    {
        SceneController.Instance.AfterSceneLoad += GetAbilityManager;
    }

    private void OnDisable()
    {
        SceneController.Instance.AfterSceneLoad -= GetAbilityManager;
        abilityManager.abilityUsed -= StartCooldown;
    }

    public void StartCooldown()
    {
        startTime = Time.time;
    }

    private void GetAbilityManager()
    {
        if (!abilityManager)
            abilityManager = FindObjectOfType<Hero>().abilityManager;
        if (abilityManager)
            abilityManager.abilityUsed += StartCooldown;
    }


}
