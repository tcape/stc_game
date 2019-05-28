using Kryz.CharacterStats.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MageStartingItems
{
    public static List<Item> startingItems = new List<Item>
    {
        Resources.Load<Item>("Items/Wand"),
        Resources.Load<Item>("Items/MagicStaff"),
        Resources.Load<Item>("Items/Staff")
    };
}
