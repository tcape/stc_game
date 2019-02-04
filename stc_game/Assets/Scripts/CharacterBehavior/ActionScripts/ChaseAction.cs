﻿using System;
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
        var distance = Math.Abs(Vector3.Distance(controller.transform.position, controller.target.transform.position));
        if (distance < controller.stats.aggroDistance)
        {
            controller.navMeshAgent.speed = controller.stats.chaseSpeed;
            controller.navMeshAgent.stoppingDistance = 4;
            // set destination to target position
            controller.navMeshAgent.destination = controller.target.transform.position;
            // look at target
            Vector3 deltaVec = controller.target.transform.position - controller.transform.position;
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(deltaVec), Time.deltaTime * controller.stats.rotationSpeed);
        }
    }
}