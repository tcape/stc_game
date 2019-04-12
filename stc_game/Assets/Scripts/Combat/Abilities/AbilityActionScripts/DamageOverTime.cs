using Assets.Scripts.CharacterBehavior.BaseClasses;
using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Ability/AbilityAction/DamageOverTime")]
public class DamageOverTime : AbilityAction
{
    private void OnEnable()
    {
        lastTick = 0;
        effectTotal = 0;
        target = null;
    }

    public override void Act(AbilityManager manager)
    {
        target.GetComponent<CharacterStats>().TakeAbilityDamage(manager.stats, amount);
        target.GetComponent<StateController>().CauseAggro();
    }

    public override void RemoveEffect(AbilityManager manager)
    {
        manager.stats.BuffDefense(-effectTotal);
    }

    public override void ResetEffectTotal()
    {
        effectTotal = 0;
    }

    public override void UpdateEffectTotal()
    {
        effectTotal += amount;
    }
}
