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
        // manager.stats.stats.BuffAttack(percentage);
        manager.heroStats.strength.attack.AddModifier(new StatModifier(percentage, ModType.PercentAdd, this));

    }

    public override void RemoveEffect(AbilityManager manager)
    {
        // manager.stats.stats.BuffAttack(-effectTotal);
        manager.heroStats.strength.attack.RemoveAllModifiersFromSource(this);
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
        //effectTotal += manager.stats.stats.attack * percentage;
    }
}
