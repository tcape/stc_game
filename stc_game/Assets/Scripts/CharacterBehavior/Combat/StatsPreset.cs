using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="PluggableAI/StatsPreset")]
public class StatsPreset : ScriptableObject
{
    public double maxHP;
    public double maxAP;
    public double strength;
    public double attack;
    public double abilityAttack;
    public double meleeCritRate;
    public double abilityCritRate;
    public double defense;
    public double dodgeRate;
    private bool dead;
}
