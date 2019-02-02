using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="PluggableAI/Decisions/StopPatrol")]
public class StopPatrolDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return StopPatrol(controller);
    }

    private bool StopPatrol(StateController controller)
    {
        var distance = Math.Abs(Vector3.Distance(controller.transform.position, controller.startPosition));
        Debug.LogError(distance);
        if (distance > .5)
        {
            Debug.LogError(distance);
            return true;
            
        }
       
        else return false;
        
    }
}
