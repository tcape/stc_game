using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Decisions/StopMeleeAttack")]
public class StopMeleeAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return OutOfMeleeRange(controller);
    }

    private bool OutOfMeleeRange(StateController controller)
    {
        var distance = Math.Abs(Vector3.Distance(controller.transform.position, controller.target.transform.position));
        if (distance > controller.stats.meleeAttackRadius)
        {
            return true;
        }
        else return false;
    }
}
