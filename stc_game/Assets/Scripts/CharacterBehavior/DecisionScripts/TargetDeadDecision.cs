using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "PluggableAI/Decisions/TargetDead")]
public class TargetDeadDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return TargetIsDead(controller);
    }

    private bool TargetIsDead(StateController controller)
    {
        if (controller.target.GetComponent<CharacterStats>().dead)
        {
            controller.target = null;
            return true;
        }
        return false;
    }
}
