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
        return (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending);
    }
}
