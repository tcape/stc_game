using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="PluggableAI/Decisions/ProximityAggro")]
public class ProximityAggroDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        return  Proximity(controller);
    }

    private bool Proximity(StateController controller)
    {
        var distance = Math.Abs(Vector3.Distance(controller.transform.position, controller.target.transform.position));
        return distance < controller.stats.aggroDistance;
    }
}
