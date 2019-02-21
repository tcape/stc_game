using Assets.Scripts.CharacterBehavior.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour, IDamageable, IHealable, IBuffable
{
    public StatsPreset presetStats;
    public double maxHP;
    public double maxAP;
    public double currentHP;
    public double currentAP;
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
        meleeCritPower = presetStats.meleeCritPower;
        abilityCritRate = presetStats.abilityCritRate;
        abilityCritPower = presetStats.abilityCritPower;
        defense = presetStats.defense;
        dodgeRate = presetStats.dodgeRate;
        dead = false;
    }

    public void TakeDamage(double amount)
    {
        if (!dead)
        {
            if (currentHP > 0)
                currentHP -= Math.Round(amount);
            if (currentHP <= 0)
            {
                currentHP = 0;
                dead = true;
            }
        }
    }

    public void TakeMeleeDamage(CharacterStats other)
    {
        TakeDamage(CalculateMeleeDamage(other));
    }

    public double CalculateMeleeDamage(CharacterStats other)
    {
        // Dodge?
        var dodgeRoll = UnityEngine.Random.Range(0.0f, 1f);
        if (dodgeRoll <= dodgeRate)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                Debug.Log("DODGE");
            }
            return 0;
        }

        // Crit?
        var critRoll = UnityEngine.Random.Range(0.0f, 1f);
        if (critRoll <= other.meleeCritRate)
        {

            if (other.gameObject.tag.Equals("Player"))
            {
                Debug.Log("CRIT");
                Debug.Log(other.attack * other.meleeCritPower - defense);
            }

            return other.attack * other.meleeCritPower - defense;
        }
        else
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                Debug.Log("NORMAL");
                Debug.Log(other.attack - defense);
            }
            return other.attack - defense;
        }
    }

    public void TakeAbilityDamage(CharacterStats other)
    {
        TakeDamage(CalculateAblilityDamage(other));
    }

    public double CalculateAblilityDamage(CharacterStats other)
    {
        throw new System.NotImplementedException();
    }

    public void Heal(double amount)
    {
        if (!dead)
        {
            if (currentHP >= 0)
                currentHP += amount;
            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
        }
    }

    public void Heal(float percentage)
    {
        Heal(maxHP * percentage);
    }

    // *************** Buffs ****************** //

    public void BuffMaxHP(double amount)
    {
        maxHP += Math.Round(amount);
    }

    public void BuffMaxHP(float percentage)
    {
        BuffMaxHP(maxHP * percentage);
    }

    public void BuffMaxAP(double amount)
    {
        maxAP += Math.Round(amount);
    }

    public void BuffMaxAP(float percentage)
    {
        BuffMaxAP(maxHP * percentage);
    }

    public void BuffCurrentHP(double amount)
    {
        currentHP += Math.Round(amount);
    }

    public void BuffCurrentHP(float percentage)
    {
        BuffCurrentHP(currentHP * percentage);
    }

    public void BuffCurrentAP(double amount)
    {
        currentAP += Math.Round(amount);
    }

    public void BuffCurrentAP(float percentage)
    {
        BuffCurrentAP(currentAP * percentage);
    }

    public void BuffStrength(double amount)
    {
        strength += Math.Round(amount);
    }

    public void BuffStrength(float percentage)
    {
        BuffStrength(strength * percentage);
    }

    public void BuffAttack(double amount)
    {
        attack += Math.Round(amount);
    }

    public void BuffAttack(float percentage)
    {
        BuffAttack(attack * percentage);
    }

    public void BuffAbilityAttack(double amount)
    {
        abilityAttack += Math.Round(amount);
    }

    public void BuffAbilityAttack(float percentage)
    {
        BuffAbilityAttack(abilityAttack * percentage);
    }

    public void BuffMeleeCritRate(double amount)
    {
        meleeCritRate += Math.Round(amount);
    }

    public void BuffMeleeCritRate(float percentage)
    {
        BuffMeleeCritRate(meleeCritRate * percentage);
    }

    public void BuffMeleeCritPower(double amount)
    {
        meleeCritPower += Math.Round(amount);
    }

    public void BuffMeleeCritPower(float percentage)
    {
        BuffMeleeCritPower(meleeCritPower * percentage);
    }

    public void BuffAbilityCritRate(double amount)
    {
        abilityCritRate += Math.Round(amount);
    }

    public void BuffAbilityCritRate(float percentage)
    {
        BuffAbilityCritRate(abilityCritRate * percentage);
    }

    public void BuffAbilityCritPower(double amount)
    {
        abilityCritPower += Math.Round(amount);
    }

    public void BuffAbilityCritPower(float percentage)
    {
        BuffAbilityCritPower(abilityCritPower * percentage);
    }

    public void BuffDefense(double amount)
    {
        defense += Math.Round(amount);
    }

    public void BuffDefense(float percentage)
    {
        BuffDefense(defense * percentage);
    }

    public void BuffDodgeRate(double amount)
    {
        dodgeRate += Math.Round(amount);
    }

    public void BuffDodgeRate(float percentage)
    {
        BuffDodgeRate(dodgeRate * percentage);
    }

    public void BuffMovementSpeed(double amount)
    {
        movementSpeed += Math.Round(amount);
    }

    public void BuffMovementSpeed(float percentage)
    {
        BuffMovementSpeed(movementSpeed * percentage);
    }
}
