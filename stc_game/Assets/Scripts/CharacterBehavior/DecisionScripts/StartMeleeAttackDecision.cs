using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="PluggableAI/Decisions/StartMeleeAttack")]
public class StartMeleeAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return InMeleeAttackRange(controller);
    }

    private bool InMeleeAttackRange(StateController controller)
    {
        if (!controller.target)
            return false;
        if (controller.target.tag.Equals("Enemy"))
        {
            var distance = Math.Abs(Vector3.Distance(controller.transform.position, controller.target.transform.position));
            return (distance <= controller.stats.meleeAttackRadius);
        }
        return false;
    }
}
