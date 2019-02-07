using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : CharacterAction
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        controller.navMeshAgent.speed = controller.stats.patrolSpeed;
        // set stopping distance to 1 so enemy makes it very close to waypoint before stopping
        controller.navMeshAgent.stoppingDistance = 1;
        controller.navMeshAgent.destination = controller.waypointList[controller.nextWayPoint].position;

        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.waypointList.Count;
        }
    }
}
