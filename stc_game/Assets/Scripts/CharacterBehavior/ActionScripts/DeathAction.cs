using Assets.Scripts.CharacterBehavior.Drops;
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
        if (!controller.animator.GetBool("Dead"))
            controller.animator.SetBool("Dead", true);
    }
}
