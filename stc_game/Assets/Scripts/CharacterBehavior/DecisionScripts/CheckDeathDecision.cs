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
        return controller.characterStats.dead;
    }
}
