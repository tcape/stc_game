using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stats
{
    [Space]
    [SerializeField] public double level;
    [SerializeField] public double XP;
    [SerializeField] public double gold;

    [SerializeField] public double currentHP;
    [SerializeField] public double currentAP;

    [Space]
    public Strength strength;
    public Intellect intellect;
    public Dexterity dexterity;

    [Space]
    public bool dead;
    [SerializeField] public double nextLevelXP;
    [SerializeField] public double totalXP;
    [HideInInspector] public static readonly double firstLevelXP = 100;

    public Stats()
    {
        Init();
    }

    public void Init()
    {
        strength = new Strength(0);
        intellect = new Intellect(0);
        dexterity = new Dexterity(0);
    }

    public void Setup()
    {
        strength.stats = this;
        intellect.stats = this;
        dexterity.stats = this;
        strength.Setup();
        intellect.Setup();
        dexterity.Setup();
    }

    public double GetNextLevel()
    {
        return nextLevelXP;
    }

    public double GetTotalXP()
    {
        return totalXP;
    }

    public void LevelUpStats()
    {
        strength.IncreaseBaseValue(level);
        intellect.IncreaseBaseValue(level);
        dexterity.IncreaseBaseValue(level);
    }

    public void RefreshHpAndAp()
    {
        currentHP = strength.maxHP.Value;
        currentAP = intellect.maxAP.Value;
    }

    public void UseAbilityPoints(double amount)
    {
        currentAP -= amount;
        if (currentAP < 0)
            currentAP = 0;
    }

    public void GainAbilityPoints(double amount)
    {
        currentAP += amount;
        if (currentAP > intellect.maxAP.Value)
            currentAP = intellect.maxAP.currentValue;
    }

    public void SetNextLevelXP()
    {
        nextLevelXP = NextLevelXPAmount();
    }

    public double NextLevelXPAmount()
    {
        if (level == 1)
            return firstLevelXP;
        return Math.Round(XP + XP * 1.5);
    }

    public void LevelUp()
    {
        level++;
        totalXP = nextLevelXP;
        SetNextLevelXP();
        LevelUpStats();
        strength.UpdateSubStatModifiers();
        dexterity.UpdateSubStatModifiers();
        intellect.UpdateSubStatModifiers();
        RefreshHpAndAp();
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
        if (dodgeRoll <= dexterity.dodgeRate.currentValue)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                Debug.Log("DODGE");
            }
            return 0;
        }

        // Crit?
        var critRoll = UnityEngine.Random.Range(0.0f, 1f);
        if (critRoll <= other.stats.dexterity.MeleeCritRate())
        {

            //if (other.gameObject.tag.Equals("Player"))
            //{
            //    Debug.Log("CRIT");
            //    Debug.Log(other.stats.strength.Attack() * other.stats.strength.MeleeCritPower() - strength.Defense());
            //}

            return other.stats.strength.Attack() * other.stats.strength.MeleeCritPower() - strength.Defense();
        }
        else
        {
            //if (other.gameObject.tag.Equals("Player"))
            //{
            //    Debug.Log("NORMAL");
            //    Debug.Log(other.stats.strength.Attack() - strength.Defense());
            //}
            return other.stats.strength.Attack() - strength.Defense();
        }
    }


    public void TakeAbilityDamage(CharacterStats other, double multiplier)
    {
        TakeDamage(CalculateAbilityDamage(other, multiplier));
    }

    public double CalculateAbilityDamage(CharacterStats other, double multiplier)
    {

        // Crit?
        var critRoll = UnityEngine.Random.Range(0.0f, 1f);
        if (critRoll <= other.stats.intellect.AbilityCritRate())
        {

            if (other.gameObject.tag.Equals("Player"))
            {
                Debug.Log("CRIT");
                Debug.Log(other.stats.intellect.AbilityAttack() * multiplier * other.stats.intellect.AbilityCritPower() - strength.Defense());
            }

            return other.stats.intellect.AbilityAttack() * multiplier * other.stats.intellect.AbilityCritPower() - strength.Defense();
        }
        else
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                Debug.Log("NORMAL");
                Debug.Log(other.stats.intellect.AbilityAttack() * multiplier - strength.defense.currentValue);
            }
            return other.stats.intellect.AbilityAttack() * multiplier - strength.defense.currentValue;
        }
    }

    public void BuffCurrentHP(double amount)
    {
        if (currentHP + Math.Round(amount) <= strength.MaxHP())
            currentHP += Math.Round(amount);
        else
            currentHP = strength.MaxHP();
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
}
