using Assets.Scripts.CharacterBehavior.BaseClasses;
using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Ability/AbilityAction/Teleport")]

public class Teleport : AbilityAction
{
    public override void Act(AbilityManager manager)
    {
        var player = manager.gameObject;
        var forward = player.transform.forward * (float)amount;
        var hero = manager.GetComponent<Hero>();
        if(NavMesh.SamplePosition(player.transform.position + forward, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            hero.navMeshAgent.Warp(hit.position);
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
