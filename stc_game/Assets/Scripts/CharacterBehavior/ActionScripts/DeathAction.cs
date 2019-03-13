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
        if (controller.GetComponent<GoldDrop>())
            controller.GetComponent<GoldDrop>().DropGold();
        controller.animator.SetBool("Dead", true);
        controller.gameObject.layer = 2;
        controller.navMeshAgent.enabled = false;
        controller.GetComponent<Rigidbody>().isKinematic = true;
        controller.GetComponent<CapsuleCollider>().enabled = false;

    }
}
