using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public class HeroStats : MonoBehaviour
//{
//    [Space]
//    [SerializeField] public double level;
//    [SerializeField] public double XP;
//    [SerializeField] public double gold;

//    [SerializeField] public double currentHP;
//    [SerializeField] public double currentAP;

//    [Space]
//    [SerializeField] public Strength strength;
//    [SerializeField] public Intellect intellect;
//    [SerializeField] public Dexterity dexterity;

//    [Space]
//    public bool dead;
//    [SerializeField] public double nextLevelXP;
//    [SerializeField] public double totalXP;
//    [HideInInspector] public static readonly double firstLevelXP = 100;

//    private void Awake()
//    {
//        SetupMainStats();
//    }

//    private void SetupMainStats()
//    {
//        strength.stats = this;
//        intellect.stats = this;
//        dexterity.stats = this;
//    }

//    public void LoadSavedStats(Stats savedStats)
//    {
//        level = savedStats.level;
//        gold = savedStats.gold;
//        XP = savedStats.XP;
//        nextLevelXP = savedStats.nextLevelXP;
//        totalXP = savedStats.totalXP;
//        strength.maxHP.baseValue = savedStats.maxHP;
//        intellect.maxAP.baseValue = savedStats.maxAP;
//        currentHP = savedStats.currentHP;
//        currentAP = savedStats.currentAP;
//        strength.baseValue = savedStats.strength;
//        intellect.baseValue = savedStats.intellect;
//        dexterity.baseValue = savedStats.dexterity;
//        strength.attack.baseValue = savedStats.attack;
//        intellect.abilityAttack.baseValue = savedStats.abilityAttack;
//        dexterity.meleeCritRate.baseValue = savedStats.meleeCritRate;
//        strength.meleeCritPower.baseValue = savedStats.meleeCritPower;
//        intellect.abilityCritRate.baseValue = savedStats.abilityCritRate;
//        intellect.abilityCritPower.baseValue = savedStats.abilityCritPower;
//        strength.defense.baseValue = savedStats.defense;
//        dexterity.dodgeRate.baseValue = savedStats.dodgeRate;
//        dexterity.movementSpeed.baseValue = savedStats.movementSpeed;
//        dead = savedStats.dead;
//    }

//    public double GetNextLevel()
//    {
//        return nextLevelXP;
//    }

//    public double GetTotalXP()
//    {
//        return totalXP;
//    }

//    public void LevelUpStats()
//    {
//        strength.IncreaseBaseValue(level);
//        intellect.IncreaseBaseValue(level);
//        dexterity.IncreaseBaseValue(level);
//    }

//    public void RefreshHpAndAp()
//    {
//        currentHP = strength.maxHP.Value;
//        currentAP = intellect.maxAP.Value;
//    }

//    public void UseAbilityPoints(double amount)
//    {
//        currentAP -= amount;
//        if (currentAP < 0)
//            currentAP = 0;
//    }

//    public void GainAbilityPoints(double amount)
//    {
//        currentAP += amount;
//        if (currentAP > intellect.maxAP.Value)
//            currentAP = intellect.maxAP.currentValue;
//    }

//    public void SetNextLevelXP()
//    {
//        nextLevelXP = NextLevelXPAmount();
//    }

//    public double NextLevelXPAmount()
//    {
//        if (level == 1)
//            return firstLevelXP;
//        return Math.Round(XP + XP * 1.5);
//    }

//    public void LevelUp()
//    {
//        level++;
//        totalXP = nextLevelXP;
//        SetNextLevelXP();
//        LevelUpStats();
//        RefreshHpAndAp();
//    }

//    public bool Ding()
//    {
//        return XP >= nextLevelXP;
//    }

//    public void GainXP(double amount)
//    {
//        XP += amount;
//        if (Ding())
//        {
//            LevelUp();
//        }
//    }

//    public void GainGold(double amount)
//    {
//        gold += amount;
//    }

//    public void LoseGold(double amount)
//    {
//        gold -= amount;
//    }

//    public void TakeDamage(double amount)
//    {
//        if (!dead)
//        {
//            if (currentHP > 0)
//                currentHP -= Math.Round(amount);
//            if (currentHP <= 0)
//            {
//                currentHP = 0;
//                dead = true;
//            }
//        }
//    }

//    public void TakeMeleeDamage(CharacterStats other)
//    {
//        TakeDamage(CalculateMeleeDamage(other));
//    }

//    public double CalculateMeleeDamage(CharacterStats other)
//    {
//        // Dodge?
//        var dodgeRoll = UnityEngine.Random.Range(0.0f, 1f);
//        if (dodgeRoll <= dexterity.dodgeRate.currentValue)
//        {
//            if (other.gameObject.tag.Equals("Player"))
//            {
//                Debug.Log("DODGE");
//            }
//            return 0;
//        }

//        // Crit?
//        var critRoll = UnityEngine.Random.Range(0.0f, 1f);
//        if (critRoll <= other.stats.meleeCritRate)
//        {

//            if (other.gameObject.tag.Equals("Player"))
//            {
//                Debug.Log("CRIT");
//                Debug.Log(other.stats.attack * other.stats.meleeCritPower - strength.defense.currentValue);
//            }

//            return other.stats.attack * other.stats.meleeCritPower - strength.defense.currentValue;
//        }
//        else
//        {
//            if (other.gameObject.tag.Equals("Player"))
//            {
//                Debug.Log("NORMAL");
//                Debug.Log(other.stats.attack - strength.defense.currentValue);
//            }
//            return other.stats.attack - strength.defense.currentValue;
//        }
//    }

//    public void TakeAbilityDamage(CharacterStats other, double multiplier)
//    {
//        TakeDamage(CalculateAbilityDamage(other, multiplier));
//    }

//    public double CalculateAbilityDamage(CharacterStats other, double multiplier)
//    {

//        // Crit?
//        var critRoll = UnityEngine.Random.Range(0.0f, 1f);
//        if (critRoll <= other.stats.abilityCritRate)
//        {

//            if (other.gameObject.tag.Equals("Player"))
//            {
//                Debug.Log("CRIT");
//                Debug.Log(other.stats.abilityAttack * multiplier * other.stats.abilityCritPower - strength.defense.currentValue);
//            }

//            return other.stats.abilityAttack * multiplier * other.stats.abilityCritPower - strength.defense.currentValue;
//        }
//        else
//        {
//            if (other.gameObject.tag.Equals("Player"))
//            {
//                Debug.Log("NORMAL");
//                Debug.Log(other.stats.abilityAttack * multiplier - strength.defense.currentValue);
//            }
//            return other.stats.abilityAttack * multiplier - strength.defense.currentValue;
//        }
//    }

//    public void Heal(double amount)
//    {
//        if (!dead)
//        {
//            if (currentHP >= 0)
//                currentHP += amount;
//            if (currentHP > strength.maxHP.currentValue)
//            {
//                currentHP = strength.maxHP.currentValue;
//            }
//        }
//    }

//    public void Heal(float percentage)
//    {
//        Heal(strength.maxHP.currentValue * percentage);
//    }
//}
