using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterBaseModel
{
    public string Name;
    public HeroClass HeroClass;
    public Stats Stats;
    public GameState GameState;
    public bool IsActive;

    public CharacterBaseModel()
    {
        Name = "Warrior";
        HeroClass = HeroClass.Warrior;
        IsActive = false;
    }

    public CharacterBaseModel(string name, HeroClass heroClass)
    {
        Name = name;
        HeroClass = heroClass;
        IsActive = false;
    }

    public CharacterBaseModel(string name, HeroClass heroClass, bool isActive)
    {
        Name = name;
        HeroClass = heroClass;
        IsActive = isActive;
    }
}


