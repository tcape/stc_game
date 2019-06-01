using System;
using System.Collections.Generic;
using Devdog.QuestSystemPro;

[Serializable]
public class GameState
{
    public class QuestState
    {
        public QuestsContainer QuestContainer = new QuestsContainer();
    }

    public class InventoryState
    {
        public List<string> Items;
    }

    public class EquipmentState
    {
        public List<string> Equipment;
    }

    public class StatsState
    {
        public Stats Stats;
    }

    public string UserId;
    public HeroClass HeroClass;

    // Add GameState save fields here

    // Quest Manager / Quest Data
    // Inventory 
    // Equipment
    // Stats

}




