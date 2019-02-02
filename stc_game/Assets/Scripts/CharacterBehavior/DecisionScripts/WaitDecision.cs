using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Decisions/Wait")]

public class WaitDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return AtWaypoint(controller);
    }

    private bool AtWaypoint(StateController controller)
    {
        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending && controller.nextWayPoint % 2 != 1)
        {
            //WaitForSeconds(controller.stats.idleWaitTime);
            return true;
        }
        else
            return false;
    }

    private IEnumerable WaitForSeconds(float seconds)
    {
        yield return WaitForSeconds(seconds);
    }
}
