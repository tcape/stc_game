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

    public GameCharacter(string name, HeroClass heroClass, Stats stats, List<string> abilities)
    {
        Name = name;
        HeroClass = heroClass;
        Stats = stats;
        Abilities = abilities;

        switch(heroClass)
        {
            case HeroClass.Warrior:
                PrefabResource = "Prefabs/WarriorPrefab";
                break;
            case HeroClass.Mage:
                PrefabResource = "Prefabs/MagePrefab";
                break;
        }
    }

    public List<Ability> GetAbilities()
    {
        var abilities = new List<Ability>();
        foreach(var str in Abilities)
        {
            abilities.Add(Resources.Load<Ability>("Abilities/" + str));
        }
        return abilities;
    }
}

