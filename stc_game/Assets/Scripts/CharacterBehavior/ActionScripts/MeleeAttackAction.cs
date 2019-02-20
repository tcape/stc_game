using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/MeleeAttack")]
public class MeleeAttackAction : CharacterAction
{
    public override void Act(StateController controller)
    {
        MeleeAttack(controller);
    }

    private void MeleeAttack(StateController controller)
    {
        if (controller.target.Equals(null))
        {
            return;
        }
        // look at target
        Vector3 deltaVec = controller.target.transform.position - controller.transform.position;
        controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(deltaVec), Time.deltaTime * controller.stats.rotationSpeed);

        controller.navMeshAgent.SetDestination(controller.transform.position);

        controller.animator.SetBool("Attacking", true);

        if (controller.animator.GetInteger("Attack").Equals(0))
        {
            controller.animator.SetInteger("Attack", 1);
        }

        else if (controller.animator.GetInteger("Attack").Equals(1))
        {
            controller.animator.SetInteger("Attack", 2);

        }
        else if (controller.animator.GetInteger("Attack").Equals(2))
        {
            controller.animator.SetInteger("Attack", 1);
        }
        
    }

}
