using Assets.Scripts.CharacterBehavior.Combat;
using Assets.Scripts.MonoBehaviors;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterStats : MonoBehaviour, IDamageable, IHealable, IBuffable
{
    private CharacterStatsSaver saver;
    public StatsPreset presetStats;
    public double level;
    public double XP;
    private double nextLevelXP;
    public double gold;
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
        saver = GetComponent<CharacterStatsSaver>();
        if (presetStats != null)
            LoadPresetStats();
    }

    private void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            CharacterStats savedStats = new CharacterStats();
            if (saver.saveData.Load(saver.key, ref savedStats))
            {
                LoadSavedStats(savedStats);
            }
        }
    }

    private void Update()
    {
        GetComponent<NavMeshAgent>().speed = (float)movementSpeed;
    }

    private void LoadSavedStats(CharacterStats savedStats)
    {
        level = savedStats.level;
        gold = savedStats.gold;
        XP = savedStats.XP;
        nextLevelXP = NextLevelXPAmount();
        maxHP = savedStats.maxHP;
        maxAP = savedStats.maxAP;
        currentHP = savedStats.currentHP;
        currentAP = savedStats.maxAP;
        strength = savedStats.strength;
        attack = savedStats.attack;
        abilityAttack = savedStats.abilityAttack;
        meleeCritRate = savedStats.meleeCritRate;
        meleeCritPower = savedStats.meleeCritPower;
        abilityCritRate = savedStats.abilityCritRate;
        abilityCritPower = savedStats.abilityCritPower;
        defense = savedStats.defense;
        dodgeRate = savedStats.dodgeRate;
        movementSpeed = savedStats.movementSpeed;
        dead = savedStats.dead;
    }

    private void LoadPresetStats()
    {
        level = presetStats.level;
        gold = presetStats.gold;
        XP = presetStats.XP;
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
        movementSpeed = presetStats.movementSpeed;
        dead = false;
    }

    private void SetNextLevelXP()
    {
        nextLevelXP = NextLevelXPAmount();
    }

    private double NextLevelXPAmount()
    {
        return Math.Round(XP + XP * 1.5);
    }

    public void LevelUp()
    {
        level++;
    }

    public bool Ding()
    {
        return XP >= nextLevelXP;
    }

    public void GainXP(double amount)
    {
        XP += amount;
        if (Ding())
        {
            LevelUp();
            SetNextLevelXP();
        }
    }

    public void GainGold(double amount)
    {
        gold += amount;
    }

    public void LoseGold(double amount)
    {
        gold -= amount;
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

    public void TakeAbilityDamage(CharacterStats other, double multiplier)
    {
        TakeDamage(CalculateAbilityDamage(other, multiplier));
    }


    public double CalculateAblilityDamage(CharacterStats other)
    {
        throw new System.NotImplementedException();
    }

    public double CalculateAbilityDamage(CharacterStats other, double multiplier)
    {
        // Crit?
        var critRoll = UnityEngine.Random.Range(0.0f, 1f);
        if (critRoll <= other.abilityCritRate)
        {

            if (other.gameObject.tag.Equals("Player"))
            {
                Debug.Log("CRIT");
                Debug.Log(other.abilityAttack * multiplier * other.abilityCritPower - defense);
            }

            return other.abilityAttack * multiplier * other.abilityCritPower - defense;
        }
        else
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                Debug.Log("NORMAL");
                Debug.Log(other.abilityAttack * multiplier - defense);
            }
            return other.abilityAttack * multiplier - defense;
        }
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
