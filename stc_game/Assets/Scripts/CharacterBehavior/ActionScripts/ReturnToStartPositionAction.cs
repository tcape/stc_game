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
        controller.navMeshAgent.stoppingDistance = 1;
        controller.navMeshAgent.destination = controller.startPosition;
    }
}
