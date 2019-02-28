using Assets.Scripts.CharacterBehavior.BaseClasses;
using Assets.Scripts.CharacterBehavior.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Ability/Ability")]
public class Ability : ScriptableObject, IAbility
{
    public enum TargetType { Self, Enemy };

    public TargetType targetType;
    public List<AbilityAction> actions;
    public string animationTrigger;
    public float cooldown;
    public float duration;
    public KeyCode hotkey;
    public float range;
    [HideInInspector] public float startTime;
    [HideInInspector] private float lastCalled = 0f;
    [HideInInspector] public GameObject target;

    public void TriggerAnimator(AbilityManager manager)
    {
        manager.animator.SetTrigger(animationTrigger);
    }

    public bool CanUse(AbilityManager manager)
    {
        if (Time.time > lastCalled + cooldown)
        {
            if (InRange(manager))
            {
                lastCalled = Time.time;
                Debug.Log(animationTrigger + " Performed");
                return true;
            }
            else
            {
                Debug.Log(animationTrigger + " Out of range");
                return false;
            }
        }
        else
        {
            Debug.Log(animationTrigger + " on Cooldown");
        }
        return false;
    }

    public bool InRange(AbilityManager manager)
    {
        if (targetType.Equals(TargetType.Self))
            return true;
        if (target == null)
            return false;
        var distance = Math.Abs(Vector3.Distance(manager.transform.position, target.transform.position));
        return (distance <= range);
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
