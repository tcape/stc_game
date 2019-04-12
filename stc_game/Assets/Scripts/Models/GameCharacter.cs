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
public class GameCharacter : MonoBehaviour
{
    public string Name;
    public HeroClass HeroClass;
    public StatsPreset StatsPreset;
    public List<Ability> Abilities;
    //public List<Item> Items;
    //public List<EquipableItem> Equipment;
    public GameObject Prefab;
    public QuestDatabase questDatabase;

}
