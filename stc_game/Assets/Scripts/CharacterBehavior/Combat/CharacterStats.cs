using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats: MonoBehaviour
{
    public StatsPreset presetStats;
    public double maxHP = 100;
    public double maxAP;
    public double currentHP = 100;
    public double currentAP;
    public double strength;
    public double attack;
    public double abilityAttack;
    public double meleeCritRate;
    public double abilityCritRate;
    public double defense;
    public double dodgeRate;
    public double movementSpeed;
    public bool dead;

    private void Awake()
    {
        maxHP = presetStats.maxHP;
        maxAP = presetStats.maxAP;
        currentHP = presetStats.maxHP;
        currentAP = presetStats.maxAP;
        strength = presetStats.strength;
        attack = presetStats.attack;
        abilityAttack = presetStats.abilityAttack;
        meleeCritRate = presetStats.meleeCritRate;
        abilityCritRate = presetStats.abilityCritRate;
        defense = presetStats.defense;
        dodgeRate = presetStats.dodgeRate;
        dead = false;
    }

    public void DoDamage(CharacterStats otherStats)
    {
        if (!dead)
        {
            if (currentHP > 0)
                currentHP -= CalculateDamage(otherStats);
            if (currentHP <= 0)
            {
                currentHP = 0;
                dead = true;
            }
        }
    }

    private double CalculateDamage(CharacterStats otherStats)
    {
        // TODO: Take stats from damage doer and this character's stats and calculate damage done
        return 5;
    }




    
}
