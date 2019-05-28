using Kryz.CharacterStats.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WarriorStartingItems
{
    public static List<Item> startingItems = new List<Item>
    {
        Resources.Load<Item>("Items/Sword"),
        Resources.Load<Item>("Items/Shield"),
        Resources.Load<Item>("Items/CrystalSword")
    };
}
