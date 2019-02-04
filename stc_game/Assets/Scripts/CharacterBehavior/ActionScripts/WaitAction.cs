using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="PluggableAI/Actions/Wait")]
public class WaitAction : CharacterAction
{
    public override void Act(StateController controller)
    {
        Wait(controller.stats.waitTime);
    }

    private IEnumerable Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
     
    }
}
