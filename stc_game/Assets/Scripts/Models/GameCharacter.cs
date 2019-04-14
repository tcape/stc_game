using Devdog.QuestSystemPro;
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
    //"public QuestDatabase QuestDatabase;
    //public List<Item> Items;
    //public List<EquipableItem> Equipment;
    public string PrefabResource;

    public GameCharacter(string name, HeroClass heroClass, Stats stats, List<string> abilities, string prefabResource)
    {
        Name = name;
        HeroClass = heroClass;
        Stats = stats;
        Abilities = abilities;
        PrefabResource = prefabResource;
       // QuestDatabase = questDatabase;
    }
}

