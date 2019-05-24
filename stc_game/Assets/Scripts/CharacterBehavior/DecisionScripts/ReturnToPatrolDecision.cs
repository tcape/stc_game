using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Decisions/ReturnToPatrol")]
public class ReturnToPatrolDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (controller.target)
            return controller.target.GetComponent<CharacterStats>().stats.dead;
        else return false;
    }
}
