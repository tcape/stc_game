using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="PluggableAI/Actions/MeleeAttack")]
public class MeleeAttackAction : CharacterAction
{
    public override void Act(StateController controller)
    {
        MeleeAttack(controller);
    }

    private void MeleeAttack(StateController controller)
    {
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
