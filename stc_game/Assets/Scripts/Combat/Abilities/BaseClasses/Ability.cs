using Assets.Scripts.CharacterBehavior.BaseClasses;
using Assets.Scripts.CharacterBehavior.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Ability/Ability")]
public class Ability : ScriptableObject, IAbility
{
    public List<AbilityAction> actions;
    public string animationTrigger;
    public float cooldown;
    public float duration;
    public KeyCode hotkey;
    [HideInInspector] public float startTime;
    [HideInInspector] private float lastCalled = 0f;
    [HideInInspector] public GameObject target;

    public void TriggerAnimator(AbilityManager manager)
    {
        manager.animator.SetTrigger(animationTrigger);
    }

    public bool CanUse()
    {
        if (Time.time > lastCalled + cooldown)
        {
            lastCalled = Time.time;
            Debug.Log(animationTrigger + " Performed");
            return true;
        }
        else
        {
            Debug.Log(animationTrigger + " on Cooldown");
        }
        return false;
    }

    private void Awake()
    {
        lastCalled = 0f;
    }

    private void OnEnable()
    {
        lastCalled = Time.time - cooldown;
    }

}
