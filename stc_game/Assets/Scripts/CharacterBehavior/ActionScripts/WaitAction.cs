using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="PluggableAI/Actions/IdleWait")]
public class IdleWait : CharacterAction
{
    public override void Act(StateController controller)
    {
        Wait(controller.stats.idleWaitTime);
    }

    private IEnumerable Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);

    }
}
