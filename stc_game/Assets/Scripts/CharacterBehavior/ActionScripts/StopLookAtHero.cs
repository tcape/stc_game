using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/StopLookAtHero")]

public class StopLookAtHero : CharacterAction
{
    public override void Act(StateController controller)
    {
        controller.navMeshAgent.destination = controller.transform.position;

        Vector3 deltaVec = controller.target.transform.position - controller.transform.position;
        if (deltaVec != Vector3.zero)
        {
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(deltaVec), Time.deltaTime * 10);
        }
    }
}
