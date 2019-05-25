using Assets.Scripts.CharacterBehavior.Drops;
using Devdog.QuestSystemPro;
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
        if (controller.characterStats.stats.dead)
        {
            Die(controller);
            return true;
        }
        return false;
    }

    private void Die(StateController controller)
    {
        if (controller.gameObject.CompareTag("Enemy"))
        {
            controller.StartCoroutine(EnemyDeath(controller));
        }
        
        if (controller.gameObject.CompareTag("Player"))
        {
            HeroDeath(controller);
        }
    }
    
    private void DisableComponents(StateController controller)
    {
        controller.animator.SetBool("Dead", true);
        controller.gameObject.layer = 2;
        controller.navMeshAgent.enabled = false;
        controller.GetComponent<Rigidbody>().isKinematic = true;
        controller.GetComponent<CapsuleCollider>().enabled = false;
    }

    private IEnumerator EnemyDeath(StateController controller)
    {
        if (controller.GetComponent<GoldDrop>())
            controller.GetComponent<GoldDrop>().DropGold();

        if (controller.GetComponent<ItemDrop>())
            controller.GetComponent<ItemDrop>().DropItems();

        controller.target.GetComponent<CharacterStats>().stats.GainXP(controller.characterStats.stats.XP);
        if (controller.gameObject.GetComponent<SetQuestProgressOnKilled>())
        {
            controller.gameObject.GetComponent<SetQuestProgressOnKilled>().OnKilled();
        }

        DisableComponents(controller);

        yield return DestroyDead(controller.transform);
    }

    private void HeroDeath(StateController controller)
    {
        PersistentScene.Instance.GameCharacter.Stats.dead = true;
        PersistentScene.Instance.reviveController.reviveCanvas.enabled = true;
        DisableComponents(controller);
    }

    private IEnumerator DestroyDead(Transform parent)
    {
        // Wait 10 seconds
        yield return new WaitForSeconds(10f);

        // Instantiate effect
        var deathEffect = Instantiate(Resources.Load("FX/DeathFX"), parent.transform.position + new Vector3(0, 0.4f, 0), Quaternion.Euler(parent.rotation.eulerAngles + new Vector3(90, 90, 0))) as GameObject;
        yield return new WaitForSeconds(1f);

        parent.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);
        Destroy(parent.gameObject);
    }
}
