using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="PluggableAI/Actions/ReturnToStart")]
public class ReturnToStartPositionAction : CharacterAction
{
    public override void Act(StateController controller)
    {
        ReturnToStart(controller);
    }

    private void ReturnToStart(StateController controller)
    {
        // set stopping distance to 1 so character gets very close to start position
        controller.animator.SetBool("Attacking", false);
        controller.navMeshAgent.stoppingDistance = 1;
        controller.navMeshAgent.SetDestination(controller.startPosition);
    }
}
