using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/Idle")]
public class IdleAction : CharacterAction
{
    public override void Act(StateController controller)
    {
        if (controller.waypointList.Count.Equals(0))
            controller.transform.rotation = controller.startRotation;

        controller.navMeshAgent.stoppingDistance = controller.stats.stoppingDistance;
    }
}
