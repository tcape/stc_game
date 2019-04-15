using Assets.Scripts.CharacterBehavior.BaseClasses;
using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityAction/BuffAttackPercent")]
public class BuffAttackPercent : AbilityAction
{
    private void OnEnable()
    {
        lastTick = 0;
        effectTotal = 0;
    }
    public override void Act(AbilityManager manager)
    {
        UpdateEffectTotalPercent(manager);
        manager.stats.stats.BuffAttack(percentage);
    }

    public override void RemoveEffect(AbilityManager manager)
    {
        manager.stats.stats.BuffAttack(-effectTotal);
    }

    public override void ResetEffectTotal()
    {
        effectTotal = 0;
    }

    public override void UpdateEffectTotal()
    {
        effectTotal += amount;
    }

    public void UpdateEffectTotalPercent(AbilityManager manager)
    {
        effectTotal += manager.stats.stats.attack * percentage;
    }
}
