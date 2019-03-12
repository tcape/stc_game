﻿using Assets.Scripts.CharacterBehavior.BaseClasses;
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
        target.GetComponent<CharacterStats>().BuffMovementSpeed(amount);
        target.GetComponent<StateController>().currentState = target.GetComponent<StateController>().aggroState;
    }

    public override void RemoveEffect(AbilityManager manager)
    {
        target.GetComponent<CharacterStats>().BuffMovementSpeed(-amount);
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
