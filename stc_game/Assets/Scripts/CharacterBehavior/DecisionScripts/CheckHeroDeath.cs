using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Decisions/CheckHeroDeath")]
public class CheckHeroDeath : Decision
{
    public override bool Decide(StateController controller)
    {
        return HeroDead();
    }

    private bool HeroDead()
    {
        var hero = GameObject.FindGameObjectWithTag("Player");
        return hero.GetComponent<CharacterStats>().stats.dead;
    }
}
