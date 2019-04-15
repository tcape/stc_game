using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu (menuName ="PluggableAI/StatsPreset")]
public class StatsPreset : ScriptableObject
{
    public double level;
    public double XP;
    public double gold;
    [Space]
    public double strength;
    public double intellect;
    public double dexterity;
    [Space]
    public double maxHP;
    public double maxAP;
    [Space]
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
