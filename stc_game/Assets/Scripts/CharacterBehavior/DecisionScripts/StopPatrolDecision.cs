using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Decisions/StopPatrol")]

public class StopPatrolDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (controller.target.GetComponent<StateController>().target == controller.gameObject)
        {
            return true;
        }
        return false;
    }
}
