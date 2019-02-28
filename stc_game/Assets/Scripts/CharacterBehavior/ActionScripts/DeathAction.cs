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
        controller.gameObject.layer = 2;
        controller.navMeshAgent.enabled = false;
        controller.GetComponent<Rigidbody>().isKinematic = true;
        controller.GetComponent<CapsuleCollider>().enabled = false;
    }
}
