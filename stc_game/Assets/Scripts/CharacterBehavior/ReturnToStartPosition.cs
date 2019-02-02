using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="PluggableAI/Actions/ReturnToStart")]
public class ReturnToStartPosition : CharacterAction
{
    public override void Act(StateController controller)
    {
        ReturnToStart(controller);
    }

    private void ReturnToStart(StateController controller)
    {
        controller.navMeshAgent.destination = controller.startPosition;
    }
}
