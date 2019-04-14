using Assets.Scripts.CharacterBehavior.Combat;
using Assets.Scripts.MonoBehaviors;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterStats : MonoBehaviour
{
    public StatsPreset presetStats;

    [SerializeField] public Stats stats;

    //[Space]
    //public double level;
    //public double XP;
    //public double gold;
    //[Space]
    //public double maxHP;
    //public double maxAP;
    //public double currentHP;
    //public double currentAP;
    //[Space]
    //public double strength;
    //public double intellect;
    //public double dexterity;

    //[Space] // Strength
    //public double attack;
    //public double meleeCritPower;
    //public double defense;

    //[Space]  // Intellect
    //public double abilityAttack;
    //public double abilityCritRate;
    //public double abilityCritPower;

    //[Space] // Dexterity
    //public double meleeCritRate;
    //public double dodgeRate;
    //public double movementSpeed;

    //[HideInInspector] public bool dead;

    public CharacterStatsSaver saver;
    //private double nextLevelXP;
    //private double totalXP = 0;
    //private static readonly double firstLevelXP = 100;

    public double GetNextLevel()
    {
        return stats.nextLevelXP;
    }

    public double GetTotalXP()
    {
        return stats.totalXP;
    }

    private void Awake()
    {
        stats = new Stats();
        
        if (gameObject.CompareTag("Player"))

        {
            saver = GetComponent<CharacterStatsSaver>();

            //if (presetStats != null)
            //{
            //    LoadPresetStats();
            //    stats.UpdateStatsEffects();
            //    //stats.RefreshHpAndAp();
            //}
        }
        else
            LoadPresetStats();

    }

    private void Start()
    {
        //if (gameObject.CompareTag("Player"))
        //{
        //    saver.Load();
        //    if(saver.characterStats != null)
        //        LoadSavedStats(saver.characterStats);
        //}

    }

    private void Update()
    {
        GetComponent<NavMeshAgent>().speed = (float)stats.movementSpeed;
    }

    public Stats GetSavedStats()
    {
        Stats savedStats = new Stats();
        if (saver.saveData.Load(saver.key, ref savedStats))
        {
            return savedStats;
        }
        return null;
    }


    public void LoadSavedStats(Stats savedStats)
    {
        stats.level = savedStats.level;
        stats.gold = savedStats.gold;
        stats.XP = savedStats.XP;
        stats.nextLevelXP = savedStats.nextLevelXP;
        stats.totalXP = savedStats.totalXP;
        stats.maxHP = savedStats.maxHP;
        stats.maxAP = savedStats.maxAP;
        stats.currentHP = savedStats.currentHP;
        stats.currentAP = savedStats.currentAP;
        stats.strength = savedStats.strength;
        stats.attack = savedStats.attack;
        stats.abilityAttack = savedStats.abilityAttack;
        stats.meleeCritRate = savedStats.meleeCritRate;
        stats.meleeCritPower = savedStats.meleeCritPower;
        stats.abilityCritRate = savedStats.abilityCritRate;
        stats.abilityCritPower = savedStats.abilityCritPower;
        stats.defense = savedStats.defense;
        stats.dodgeRate = savedStats.dodgeRate;
        stats.movementSpeed = savedStats.movementSpeed;
        stats.dead = savedStats.dead;
    }

    public void LoadPresetStats(StatsPreset presetStats)
    {
        stats.level = presetStats.level;
        stats.gold = presetStats.gold;
        stats.XP = presetStats.XP;
        stats.nextLevelXP = stats.NextLevelXPAmount();
        stats.maxHP = presetStats.maxHP;
        stats.maxAP = presetStats.maxAP;
        stats.currentHP = presetStats.maxHP;
        stats.currentAP = presetStats.maxAP;
        stats.strength = presetStats.strength;
        stats.intellect = presetStats.intellect;
        stats.dexterity = presetStats.dexterity;
        stats.attack = presetStats.attack;
        stats.abilityAttack = presetStats.abilityAttack;
        stats.meleeCritRate = presetStats.meleeCritRate;
        stats.meleeCritPower = presetStats.meleeCritPower;
        stats.abilityCritRate = presetStats.abilityCritRate;
        stats.abilityCritPower = presetStats.abilityCritPower;
        stats.defense = presetStats.defense;
        stats.dodgeRate = presetStats.dodgeRate;
        stats.movementSpeed = presetStats.movementSpeed;
        stats.dead = false;
    }

    public void LoadPresetStats()
    {
        stats.level = presetStats.level;
        stats.gold = presetStats.gold;
        stats.XP = presetStats.XP;
        stats.nextLevelXP = stats.NextLevelXPAmount();
        stats.maxHP = presetStats.maxHP;
        stats.maxAP = presetStats.maxAP;
        stats.currentHP = presetStats.maxHP;
        stats.currentAP = presetStats.maxAP;
        stats.strength = presetStats.strength;
        stats.intellect = presetStats.intellect;
        stats.dexterity = presetStats.dexterity;
        stats.attack = presetStats.attack;
        stats.abilityAttack = presetStats.abilityAttack;
        stats.meleeCritRate = presetStats.meleeCritRate;
        stats.meleeCritPower = presetStats.meleeCritPower;
        stats.abilityCritRate = presetStats.abilityCritRate;
        stats.abilityCritPower = presetStats.abilityCritPower;
        stats.defense = presetStats.defense;
        stats.dodgeRate = presetStats.dodgeRate;
        stats.movementSpeed = presetStats.movementSpeed;
        stats.dead = false;
    }
}
//    public void LevelUpStats(double str, double intel, double dex)
//    {
//        stats.strength += str;
//        stats.intellect += intel;
//        stats.dexterity += dex;
//    }

//    public void LevelUpStats()
//    {
//        stats.strength += stats.level;
//        stats.intellect += stats.level;
//        stats.dexterity += stats.level;
//    }

//    public void IncreseStrength(double amount)
//    {
//        stats.strength += amount;
//    }

//    public void IncreaseIntellect(double amount)
//    {
//        stats.intellect += amount;
//    }

//    public void IncreaseDexterity(double amount)
//    {
//        stats.dexterity += amount;
//    }

//    public void RefreshHpAndAp()
//    {
//        stats.currentHP = stats.maxHP;
//        stats.currentAP = stats.maxAP;
//    }

//    public void UpdateStatsEffects()
//    {
//        StrengthStatsEffect();
//        IntellectStatsEffect();
//        DexterityStatsEffect();
//    }

//    private void StrengthStatsEffect()
//    {
//        stats.attack += Math.Round(stats.attack * stats.strength / stats.nextLevelXP);
//        stats.meleeCritPower += Math.Round(stats.meleeCritPower * stats.strength / stats.nextLevelXP);
//        stats.defense += Math.Round(stats.defense * stats.strength / stats.nextLevelXP);
//        stats.maxHP += Math.Round(stats.maxHP * stats.strength / stats.nextLevelXP);
//    }

//    private void IntellectStatsEffect()
//    {
//        stats.abilityAttack += Math.Round(stats.abilityAttack * stats.intellect / stats.nextLevelXP);
//        stats.abilityCritPower += Math.Round(stats.abilityCritPower * stats.intellect / stats.nextLevelXP);
//        stats.abilityCritRate += Math.Round(stats.abilityCritRate * stats.intellect / stats.nextLevelXP);
//        stats.maxAP += Math.Round(stats.maxAP * stats.intellect / stats.nextLevelXP);
//    }

//    private void DexterityStatsEffect()
//    {
//        stats.dodgeRate += Math.Round(stats.dodgeRate * stats.dexterity / stats.nextLevelXP);
//        stats.meleeCritRate += Math.Round(stats.meleeCritRate * stats.dexterity / stats.nextLevelXP);
//        stats.movementSpeed += Math.Round(stats.movementSpeed * stats.dexterity / stats.nextLevelXP);
//    }


//    public void UseAbilityPoints(double amount)
//    {
//        stats.currentAP -= amount;
//        if (stats.currentAP < 0)
//            stats.currentAP = 0;
//    }

//    public void GainAbilityPoints(double amount)
//    {
//        stats.currentAP += amount;
//        if (stats.currentAP > stats.maxAP)
//            stats.currentAP = stats.maxAP;
//    }
    
//    private void SetNextLevelXP()
//    {
//        stats.nextLevelXP = NextLevelXPAmount();
//    }

//    private double NextLevelXPAmount()
//    {
//        if (stats.level == 1)
//            return stats.firstLevelXP;
//        return Math.Round(stats.XP + stats.XP * 1.5);
//    }

//    public void LevelUp()
//    {
//        stats.level++;
//        totalXP = nextLevelXP;
//        SetNextLevelXP();
//        LevelUpStats();
//        UpdateStatsEffects();
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
//        if (dodgeRoll <= dodgeRate)
//        {
//            if (other.gameObject.tag.Equals("Player"))
//            {
//                Debug.Log("DODGE");
//            }
//            return 0;
//        }

//        // Crit?
//        var critRoll = UnityEngine.Random.Range(0.0f, 1f);
//        if (critRoll <= other.meleeCritRate)
//        {

//            if (other.gameObject.tag.Equals("Player"))
//            {
//                Debug.Log("CRIT");
//                Debug.Log(other.attack * other.meleeCritPower - defense);
//            }

//            return other.attack * other.meleeCritPower - defense;
//        }
//        else
//        {
//            if (other.gameObject.tag.Equals("Player"))
//            {
//                Debug.Log("NORMAL");
//                Debug.Log(other.attack - defense);
//            }
//            return other.attack - defense;
//        }
//    }

//    public void TakeAbilityDamage(CharacterStats other)
//    {
//        TakeDamage(CalculateAblilityDamage(other));
//    }

//    public void TakeAbilityDamage(CharacterStats other, double multiplier)
//    {
//        TakeDamage(CalculateAbilityDamage(other, multiplier));
//    }


//    public double CalculateAblilityDamage(CharacterStats other)
//    {
//        throw new System.NotImplementedException();
//    }

//    public double CalculateAbilityDamage(CharacterStats other, double multiplier)
//    {
//        // Crit?
//        var critRoll = UnityEngine.Random.Range(0.0f, 1f);
//        if (critRoll <= other.abilityCritRate)
//        {

//            if (other.gameObject.tag.Equals("Player"))
//            {
//                Debug.Log("CRIT");
//                Debug.Log(other.abilityAttack * multiplier * other.abilityCritPower - defense);
//            }

//            return other.abilityAttack * multiplier * other.abilityCritPower - defense;
//        }
//        else
//        {
//            if (other.gameObject.tag.Equals("Player"))
//            {
//                Debug.Log("NORMAL");
//                Debug.Log(other.abilityAttack * multiplier - defense);
//            }
//            return other.abilityAttack * multiplier - defense;
//        }
//    }

//    public void Heal(double amount)
//    {
//        if (!dead)
//        {
//            if (currentHP >= 0)
//                currentHP += amount;
//            if (currentHP > maxHP)
//            {
//                currentHP = maxHP;
//            }
//        }
//    }

//    public void Heal(float percentage)
//    {
//        Heal(maxHP * percentage);
//    }

//    // *************** Buffs ****************** //

//    public void BuffMaxHP(double amount)
//    {
//        maxHP += Math.Round(amount);
//    }

//    public void BuffMaxHP(float percentage)
//    {
//        BuffMaxHP(maxHP * percentage);
//    }

//    public void BuffMaxAP(double amount)
//    {
//        maxAP += Math.Round(amount);
//    }

//    public void BuffMaxAP(float percentage)
//    {
//        BuffMaxAP(maxHP * percentage);
//    }

//    public void BuffCurrentHP(double amount)
//    {
//        currentHP += Math.Round(amount);
//    }

//    public void BuffCurrentHP(float percentage)
//    {
//        BuffCurrentHP(currentHP * percentage);
//    }

//    public void BuffCurrentAP(double amount)
//    {
//        currentAP += Math.Round(amount);
//    }

//    public void BuffCurrentAP(float percentage)
//    {
//        BuffCurrentAP(currentAP * percentage);
//    }

//    public void BuffStrength(double amount)
//    {
//        strength += Math.Round(amount);
//    }

//    public void BuffStrength(float percentage)
//    {
//        BuffStrength(strength * percentage);
//    }

//    public void BuffAttack(double amount)
//    {
//        attack += Math.Round(amount);
//    }

//    public void BuffAttack(float percentage)
//    {
//        BuffAttack(attack * percentage);
//    }

//    public void BuffAbilityAttack(double amount)
//    {
//        abilityAttack += Math.Round(amount);
//    }

//    public void BuffAbilityAttack(float percentage)
//    {
//        BuffAbilityAttack(abilityAttack * percentage);
//    }

//    public void BuffMeleeCritRate(double amount)
//    {
//        meleeCritRate += Math.Round(amount);
//    }

//    public void BuffMeleeCritRate(float percentage)
//    {
//        BuffMeleeCritRate(meleeCritRate * percentage);
//    }

//    public void BuffMeleeCritPower(double amount)
//    {
//        meleeCritPower += Math.Round(amount);
//    }

//    public void BuffMeleeCritPower(float percentage)
//    {
//        BuffMeleeCritPower(meleeCritPower * percentage);
//    }

//    public void BuffAbilityCritRate(double amount)
//    {
//        abilityCritRate += Math.Round(amount);
//    }

//    public void BuffAbilityCritRate(float percentage)
//    {
//        BuffAbilityCritRate(abilityCritRate * percentage);
//    }

//    public void BuffAbilityCritPower(double amount)
//    {
//        abilityCritPower += Math.Round(amount);
//    }

//    public void BuffAbilityCritPower(float percentage)
//    {
//        BuffAbilityCritPower(abilityCritPower * percentage);
//    }

//    public void BuffDefense(double amount)
//    {
//        defense += Math.Round(amount);
//    }

//    public void BuffDefense(float percentage)
//    {
//        BuffDefense(defense * percentage);
//    }

//    public void BuffDodgeRate(double amount)
//    {
//        dodgeRate += Math.Round(amount);
//    }

//    public void BuffDodgeRate(float percentage)
//    {
//        BuffDodgeRate(dodgeRate * percentage);
//    }

//    public void BuffMovementSpeed(double amount)
//    {
//        movementSpeed += Math.Round(amount);
//    }

//    public void BuffMovementSpeed(float percentage)
//    {
//        BuffMovementSpeed(movementSpeed * percentage);
//    }
//}
