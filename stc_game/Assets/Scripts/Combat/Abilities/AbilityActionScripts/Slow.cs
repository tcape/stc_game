using Assets.Scripts.CharacterBehavior.BaseClasses;
using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityAction/Slow")]
public class Slow : AbilityAction
{
    private void OnEnable()
    {
        lastTick = 0;
        effectTotal = 0;
    }

    public override void Act(AbilityManager manager)
    {
        target.GetComponent<CharacterStats>().stats.dexterity.movementSpeed.AddModifier(new StatModifier(amount, ModType.Flat, this));
        if (!target.GetComponent<CharacterStats>().stats.dead)
            target.GetComponent<StateController>().CauseAggro();
    }

    public override void RemoveEffect(AbilityManager manager)
    {
        target.GetComponent<CharacterStats>().stats.dexterity.movementSpeed.RemoveAllModifiersFromSource(this);
    }

    public override void ResetEffectTotal()
    {
        return;
    }

    public override void UpdateEffectTotal()
    {
        return;
    }
}
