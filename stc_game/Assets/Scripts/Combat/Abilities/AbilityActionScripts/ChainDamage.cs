using Assets.Scripts.CharacterBehavior.BaseClasses;
using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityAction/ChainDamage")]
public class ChainDamage : AbilityAction
{
    private List<GameObject> damaged;

    private void OnEnable()
    {
        lastTick = 0;
        effectTotal = 0;
        damaged = new List<GameObject>();
    }

    public override void Act(AbilityManager manager)
    {
        target.GetComponent<CharacterStats>().stats.TakeDamage(amount);
        damaged.Add(target);

        //if target's collider is touching others, they take damage too
        foreach (var enemy in target.GetComponent<HitboxCollision>().touching)
        {
            if (!damaged.Contains(enemy))
            {
                enemy.GetComponent<CharacterStats>().stats.TakeDamage(amount);
                damaged.Add(enemy);
            }
                
            // and enemies they touch
            foreach (var e in enemy.GetComponent<HitboxCollision>().touching)
                if (!damaged.Contains(e))
                {
                    e.GetComponent<CharacterStats>().stats.TakeDamage(amount);
                    damaged.Add(e);
                }
        }

        foreach (var enemy in damaged)
        {
            enemy.GetComponent<StateController>().CauseAggro();
        }

        damaged.Clear();
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
