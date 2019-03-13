using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="PluggableAI/StatsPreset")]
public class StatsPreset : ScriptableObject
{
    public double level;
    public double gold;
    public double XP;
    public double maxHP;
    public double maxAP;
    public double strength;
    public double attack;
    public double abilityAttack;
    public double meleeCritRate;
    public double meleeCritPower;
    public double abilityCritRate;
    public double abilityCritPower;
    public double defense;
    public double dodgeRate;
    public double movementSpeed;
    private bool dead;
}
