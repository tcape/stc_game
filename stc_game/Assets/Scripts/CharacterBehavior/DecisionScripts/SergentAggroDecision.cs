using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Decisions/SergentAggro")]
public class SergentAggroDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return SergentIsAggro(controller);
    }

    private bool SergentIsAggro(StateController controller)
    {
        return (controller.aggroSergent.GetComponent<StateController>().currentState.isAggro);
    }
}
