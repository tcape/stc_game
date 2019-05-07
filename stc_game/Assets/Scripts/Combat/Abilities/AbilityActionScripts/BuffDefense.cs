using Assets.Scripts.CharacterBehavior.BaseClasses;
using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityAction/BuffDefense")]
public class BuffDefense : AbilityAction
{
    private void OnEnable()
    {
        lastTick = 0;
        effectTotal = 0;
    }

    public override void Act(AbilityManager manager)
    {
        manager.stats.stats.strength.defense.AddModifier(new StatModifier(amount, ModType.Flat, this));
        UpdateEffectTotal();
    }

    public override void RemoveEffect(AbilityManager manager)
    {
        manager.stats.stats.strength.defense.RemoveAllModifiersFromSource(this);
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
