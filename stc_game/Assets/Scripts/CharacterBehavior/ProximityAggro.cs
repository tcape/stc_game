using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="PluggableAI/Decisions/ProximityAggro")]
public class ProximityAggro : Decision
{
    private bool aggro = false;

    public override bool Decide(StateController controller)
    {
        return  Proximity(controller);
    }

    private bool Proximity(StateController controller)
    {
        var distance = Math.Abs(Vector3.Distance(controller.transform.position, controller.target.transform.position));

        if (distance < controller.stats.aggroDistance)
        {
            aggro = true;
        }

        else
        {
            aggro = false;
        }

        return aggro;
    }
}
