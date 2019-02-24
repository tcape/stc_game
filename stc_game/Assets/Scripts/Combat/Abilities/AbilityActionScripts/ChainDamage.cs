using Assets.Scripts.CharacterBehavior.BaseClasses;
using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityAction/ChainDamage")]
public class ChainDamage : AbilityAction
{
    private void OnEnable()
    {
        lastTick = 0;
        effectTotal = 0;
    }

    public override void Act(AbilityManager manager)
    {
        target.GetComponent<CharacterStats>().TakeDamage(amount);

        //if target's collider is touching others, they take damage too
        foreach (var enemy in target.GetComponent<HitboxCollision>().touching)
        {
            enemy.GetComponent<CharacterStats>().TakeDamage(amount);
            // and enemies they touch
            foreach (var e in enemy.GetComponent<HitboxCollision>().touching)
                e.GetComponent<CharacterStats>().TakeDamage(amount);
        }
    }

    public override void RemoveEffect(AbilityManager manager)
    {
        return;
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
