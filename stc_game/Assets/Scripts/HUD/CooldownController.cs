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

    private void Awake()
    {
        cooldown = GetComponent<Image>();
        cooldown.fillAmount = 0;
    }

    private void OnGUI()
    {
        ability = abilitySlot.GetComponent<AbilitySlot>().ability;
       
    }

    private void Update()
    {
        if (ability.startTime != 0f)
        {
            if (ability.startTime <= Time.time)
                cooldown.fillAmount = 1f - (Time.time - ability.startTime) / ability.cooldown;
        }
        
    }
}
