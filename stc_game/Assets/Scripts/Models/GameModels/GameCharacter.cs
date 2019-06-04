using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HeroClass
{
    Warrior,
    Mage
}

[Serializable]
public class GameCharacter
{
    public string Name;
    public HeroClass HeroClass;
    public Stats Stats;
    public List<string> Abilities;
    public string PrefabResource;
    //public List<Item> Items;
    //public List<EquipableItem> Equipment;

    public GameCharacter(string name, HeroClass heroClass)
    {
        Name = name;
        HeroClass = heroClass;

        if (heroClass == HeroClass.Warrior)
        {
            Stats = GetWarriorPresetStats();
            Abilities = GetWarriorListAbilities();
            PrefabResource = "Prefabs/WarriorPrefab";
        }
        else if (heroClass == HeroClass.Mage)
        {
            Stats = GetMagePresetStats();
            Abilities = GetMageListAbilities();
            PrefabResource = "Prefabs/MagePrefab";
        }
        Stats.Setup();
    }

    public GameCharacter(string name, HeroClass heroClass, Stats stats)
    {
        Name = name;
        HeroClass = heroClass;
        Stats = stats;

        if (heroClass == HeroClass.Warrior)
        {
            Abilities = GetWarriorListAbilities();
            PrefabResource = "Prefabs/WarriorPrefab";
        }
        else if (heroClass == HeroClass.Mage)
        {
            Abilities = GetMageListAbilities();
            PrefabResource = "Prefabs/MagePrefab";
        }
    }

    public List<Ability> GetAbilities()
    {
        var abilities = new List<Ability>();
        foreach (var str in Abilities)
        {
            abilities.Add(Resources.Load<Ability>("Abilities/" + str));
        }
        return abilities;
    }

    public Stats GetStatsFromData(Stats data)
    {
        return new Stats()
        {
            level = data.level,
            XP = data.XP,
            gold = data.gold,
            strength = new Strength(data.strength.baseValue)
            {
                maxHP = new SubStat(data.strength.maxHP.baseValue),
                attack = new SubStat(data.strength.attack.baseValue),
                meleeCritPower = new SubStat(data.strength.meleeCritPower.baseValue),
                defense = new SubStat(data.strength.defense.baseValue)
            },
            intellect = new Intellect(data.intellect.baseValue)
            {
                maxAP = new SubStat(data.intellect.maxAP.baseValue),
                abilityAttack = new SubStat(data.intellect.abilityAttack.baseValue),
                abilityCritPower = new SubStat(data.intellect.abilityCritPower.baseValue),
                abilityCritRate = new SubStat(data.intellect.abilityCritRate.baseValue)
            },
            dexterity = new Dexterity(data.dexterity.baseValue)
            {
                dodgeRate = new SubStat(data.dexterity.dodgeRate.baseValue),
                meleeCritRate = new SubStat(data.dexterity.meleeCritRate.baseValue),
                movementSpeed = new SubStat(data.dexterity.movementSpeed.baseValue)
            },
            currentHP = data.currentHP,
            currentAP = data.currentAP,
            dead = data.dead,
            nextLevelXP = data.nextLevelXP
        };
    }

    private Stats GetMagePresetStats()
    {
        return new Stats()
        {
            level = 1,
            XP = 0,
            gold = 0,
            strength = new Strength(5)
            {
                maxHP = new SubStat(500),
                attack = new SubStat(6),
                meleeCritPower = new SubStat(1.5),
                defense = new SubStat(5)
            },
            intellect = new Intellect(10)
            {
                maxAP = new SubStat(180),
                abilityAttack = new SubStat(15),
                abilityCritPower = new SubStat(2),
                abilityCritRate = new SubStat(0.25)
            },
            dexterity = new Dexterity(5)
            {
                dodgeRate = new SubStat(0.15),
                meleeCritRate = new SubStat(0.25),
                movementSpeed = new SubStat(5)
            },
            currentHP = 500,
            currentAP = 180,
            dead = false,
            nextLevelXP = 100
        };
    }

    private List<string> GetMageListAbilities()
    {
        return new List<string>()
        {
             "RegenerateAP",
             "Fireball",
             "Curse",
             "Slow",
             "Teleport"
        };
    }

    private Stats GetWarriorPresetStats()
    {
        return new Stats()
        {
            level = 1,
            XP = 0,
            gold = 0,
            strength = new Strength(10)
            {
                maxHP = new SubStat(500),
                attack = new SubStat(15),
                meleeCritPower = new SubStat(2),
                defense = new SubStat(9)
            },
            intellect = new Intellect(5)
            {
                maxAP = new SubStat(150),
                abilityAttack = new SubStat(10),
                abilityCritPower = new SubStat(2),
                abilityCritRate = new SubStat(0.25)
            },
            dexterity = new Dexterity(7)
            {
                dodgeRate = new SubStat(0.15),
                meleeCritRate = new SubStat(0.25),
                movementSpeed = new SubStat(7)
            },
            currentHP = 500,
            currentAP = 150,
            dead = false,
            nextLevelXP = 100
        };
    }

    private List<string> GetWarriorListAbilities()
    {
        return new List<string>()
        {
             "RegenerateAP",
             "IronSkin",
             "Savagry",
             "Cleave",
             "Sprint"
        };
    }
}

