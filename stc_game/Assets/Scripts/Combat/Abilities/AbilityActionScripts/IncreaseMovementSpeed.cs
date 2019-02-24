﻿using Assets.Scripts.CharacterBehavior.BaseClasses;
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
        manager.stats.BuffMovementSpeed(amount);
        UpdateEffectTotal();
    }

    public override void RemoveEffect(AbilityManager manager)
    {
        manager.stats.BuffMovementSpeed(-effectTotal);
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
