using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Decisions/BackAtStart")]
public class BackAtStartDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return AtStart(controller);
    }

    private bool AtStart(StateController controller)
    {
        controller.navMeshAgent.stoppingDistance = controller.stats.stoppingDistance;
        return (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance);
    }
}
