using Assets.Scripts.CharacterBehavior.Drops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/CheckDeath")]
public class CheckDeathDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return CheckDeath(controller);
    }

    private bool CheckDeath(StateController controller)
    {
        if (controller.characterStats.dead)
        {
            Die(controller);
            return true;
        }

        return false;
    }

    private void Die(StateController controller)
    {
        if (controller.GetComponent<GoldDrop>())
            controller.GetComponent<GoldDrop>().DropGold();

        if (controller.gameObject.CompareTag("Enemy"))
        {
            controller.target.GetComponent<CharacterStats>().GainXP(controller.characterStats.XP);
        }
        
        controller.animator.SetBool("Dead", true);
        controller.gameObject.layer = 2;
        controller.navMeshAgent.enabled = false;
        controller.GetComponent<Rigidbody>().isKinematic = true;
        controller.GetComponent<CapsuleCollider>().enabled = false;
    }
}
