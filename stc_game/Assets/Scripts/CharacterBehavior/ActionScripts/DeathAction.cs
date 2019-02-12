using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Death")]
public class DeathAction : CharacterAction
{
    public override void Act(StateController controller)
    {
        Die(controller);
    }

    public void Die(StateController controller)
    {
        controller.animator.SetBool("Dead", true);
    }
}
