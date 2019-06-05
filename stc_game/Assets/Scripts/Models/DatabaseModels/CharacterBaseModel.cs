using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterBaseModel
{
    public string Name;
    public HeroClass HeroClass;
    public GameState GameState;
    public bool IsActive;

    public CharacterBaseModel()
    {
        HeroClass = HeroClass.Warrior;
        Name = HeroClass.ToString();
        GameState = new GameState();
        IsActive = true;
    }

    public CharacterBaseModel(string name, HeroClass heroClass)
    {
        Name = name;
        HeroClass = heroClass;
        GameState = new GameState();
        IsActive = false;
    }

    public CharacterBaseModel(string name, HeroClass heroClass, bool isActive)
    {
        Name = name;
        HeroClass = heroClass;
        GameState = new GameState();
        IsActive = isActive;
    }
}


