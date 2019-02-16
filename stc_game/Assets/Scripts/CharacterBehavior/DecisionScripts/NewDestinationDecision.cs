using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewDestinationDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return NewDestinationSet(controller);
    }

    private bool NewDestinationSet(StateController controller)
    {

        if (Input.GetMouseButton(0))
        {

        }
        return false;
    }
}
