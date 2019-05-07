using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Decisions/ReturnToPatrol")]
public class ReturnToPatrolDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (controller.target.GetComponent<StateController>().target == controller.gameObject)
        {
            return false;
        }
        return true;
    }
}
