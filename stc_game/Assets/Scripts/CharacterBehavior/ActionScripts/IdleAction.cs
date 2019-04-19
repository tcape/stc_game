using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/Idle")]
public class IdleAction : CharacterAction
{
    private GameObject hero;

    public override void Act(StateController controller)
    {
        if (hero == null)
            hero = GameObject.FindGameObjectWithTag("Player");

        if (controller.waypointList.Count.Equals(0) && controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance)
            controller.transform.rotation = controller.startRotation;

        controller.navMeshAgent.stoppingDistance = controller.stats.stoppingDistance;

        if (controller.gameObject.CompareTag("Enemy") && controller.target == null)
        {
            if (!hero.GetComponent<Hero>().characterStats.stats.dead)
                controller.target = hero.gameObject;
        }
    }
}
