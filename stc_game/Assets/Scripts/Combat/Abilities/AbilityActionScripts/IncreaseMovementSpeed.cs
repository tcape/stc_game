using Assets.Scripts.CharacterBehavior.BaseClasses;
using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Ability/AbilityAction/IncreaseSpeed")]
public class IncreaseMovementSpeed : AbilityAction
{
    private void OnEnable()
    {
        lastTick = 0;
        effectTotal = 0;
    }
    public override void Act(AbilityManager manager)
    {
        UpdateEffectTotal();
        // manager.stats.stats.BuffMovementSpeed(amount);
        manager.stats.stats.dexterity.movementSpeed.AddModifier(new StatModifier(amount, ModType.Flat, this));
    }

    public override void RemoveEffect(AbilityManager manager)
    {
        // manager.stats.stats.BuffMovementSpeed(-effectTotal);
        manager.stats.stats.dexterity.movementSpeed.RemoveAllModifiersFromSource(this);
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
