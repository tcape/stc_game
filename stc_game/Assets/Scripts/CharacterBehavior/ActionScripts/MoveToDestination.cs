using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "PluggableAI/Actions/MoveToDestination")]
public class MoveToDestination : CharacterAction
{
    public override void Act(StateController controller)
    {
        GetInputAndMove(controller);
    }

    private void GetInputAndMove(StateController controller)
    {
        controller.animator.SetBool("Attacking", false);
        controller.animator.SetInteger("Attack", 0);

        // if left-click
        if (Input.GetMouseButton(0))
        {
            // raycast at mouse position
            Ray ray = controller.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if hit something
            if (Physics.Raycast(ray, out hit))
            {
                // if hit enemy or npc set target
                if (hit.collider.gameObject.tag.Equals("Enemy") || hit.collider.gameObject.tag.Equals("NPC"))
                {
                    controller.target = hit.collider.gameObject;
                }
                else
                {
                    // set destination of nav mesh agent
                    controller.navMeshAgent.SetDestination(hit.point);
                    controller.target = null;

                }
            }
        }

        // if right-click
        if (Input.GetMouseButton(1))
        {
            // raycast at mouse position
            Ray ray = controller.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if hit something get the hit
            if (Physics.Raycast(ray, out hit))
            {
                // if hit enemy or boss, set hero's target
                if (hit.collider.gameObject.tag.Equals("Enemy") || hit.collider.gameObject.tag.Equals("NPC"))
                {
                    controller.target = hit.collider.gameObject;
                    var distance = Math.Abs(Vector3.Distance(controller.transform.position, controller.target.transform.position));

                    if (distance > controller.stats.stoppingDistance)
                        controller.navMeshAgent.SetDestination(controller.target.transform.position);
                }
                // otherwise, set hero's target to null
                else
                {
                    controller.target = null;
                }
            }
        }

        // if target is null, return
        if (controller.target == null)
        {
            controller.navMeshAgent.stoppingDistance = 1f;
            // rotation bug
            if (controller.navMeshAgent.velocity.magnitude < 0.1f)
                controller.navMeshAgent.angularSpeed = 0;
            else
                controller.navMeshAgent.angularSpeed = controller.stats.rotationSpeed;
            return;
        }
        // otherwise, check speed
        else
        {
            var speed = controller.navMeshAgent.velocity.magnitude;
            // if speed is very slow or stopped, look at the target

            if (speed < 0.1f)
            {
                Vector3 deltaVec = controller.target.transform.position - controller.transform.position;
                if (deltaVec != Vector3.zero)
                {
                   controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(deltaVec), Time.deltaTime * 10);
                }
            }
        }

        if (controller.target.gameObject.tag.Equals("Enemy"))
        {
            controller.navMeshAgent.stoppingDistance = controller.stats.stoppingDistance;
            var distance = Math.Abs(Vector3.Distance(controller.transform.position, controller.target.transform.position));
            if (distance > controller.stats.stoppingDistance && distance <= controller.stats.meleeAttackRadius)
                controller.navMeshAgent.SetDestination(controller.target.transform.position);
        }
        
    }
}
