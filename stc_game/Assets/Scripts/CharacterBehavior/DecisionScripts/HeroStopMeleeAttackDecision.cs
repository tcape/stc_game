using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "PluggableAI/Decisions/HeroStopAttacking")]
public class HeroStopMeleeAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return NotAttacking(controller);
    }

    private bool NotAttacking(StateController controller)
    {
        if (controller.target.Equals(null))
            return true;

        // if left-click
        if (Input.GetMouseButton(0))
        {
            // raycast at mouse position
            Ray ray = controller.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if hit something
            if (Physics.Raycast(ray, out hit))
            {
                return true;
            }
        }

        if (Input.GetMouseButton(1))
        {
            // raycast at mouse position
            Ray ray = controller.cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if hit something
            if (Physics.Raycast(ray, out hit))
            {
                // if hit enemy or boss
                if (hit.rigidbody.gameObject.tag.Equals("Enemy") || hit.rigidbody.gameObject.tag.Equals("Boss1"))
                    return false;

                else return true;
            }
        }

        return false;
    }
}
