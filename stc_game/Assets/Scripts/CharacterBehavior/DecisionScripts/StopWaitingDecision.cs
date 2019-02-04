using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Decisions/StopWaiting")]
public class StopWaitingDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return controller.CheckIfCountDownElapsed(controller.stats.waitTime);
    }
}
