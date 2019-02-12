using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/StopChasing")]
public class StopChasingDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return OutOfRange(controller);
    }

    private bool OutOfRange(StateController controller)
    {
        var distance = Math.Abs(Vector3.Distance(controller.transform.position, controller.target.transform.position));

        return (distance > controller.stats.stopFollowDistance);
    }
}
