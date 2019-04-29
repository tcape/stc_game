using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterBaseModel
{
    public string Name;
    public HeroClass HeroClass;
    public Stats stats;
    public List<string> Abilities;
    public GameState GameState;
}
