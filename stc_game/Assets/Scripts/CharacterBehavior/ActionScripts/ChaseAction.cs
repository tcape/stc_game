using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="PluggableAI/Actions/Chase")]
public class ChaseAction : CharacterAction
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase (StateController controller)
    {
        controller.navMeshAgent.speed = (float)controller.GetComponent<CharacterStats>().stats.dexterity.MovementSpeed();
        controller.navMeshAgent.stoppingDistance = controller.stats.stoppingDistance;
        controller.animator.SetBool("Attacking", false);
        controller.animator.SetInteger("Attack", 0);
        // set destination to target position
        controller.target = GameObject.FindGameObjectWithTag("Player");
        controller.navMeshAgent.destination = controller.target.transform.position;
        // look at target
        Vector3 deltaVec = controller.target.transform.position - controller.transform.position;
        controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(deltaVec), Time.deltaTime * controller.stats.rotationSpeed);
    }
}
