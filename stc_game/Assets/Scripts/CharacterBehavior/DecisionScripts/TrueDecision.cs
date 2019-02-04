using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="PluggableAI/Decisions/True")]
public class TrueDecision : Decision
{
   

    public override bool Decide(StateController controller)
    {
        return true;
    }

    //public IEnumerable Wait(StateController controller)
    //{
       

    //}
}
