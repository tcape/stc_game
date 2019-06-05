using System;
using System.Collections.Generic;
using Devdog.QuestSystemPro;

[Serializable]
public class GameState
{
    public QuestsContainer QuestsContainer;
    public List<string> Items;
    public List<string> EquippedItems;
    public Stats Stats;
    public bool isDirty = false;

    public GameState()
    {
        isDirty = false;
    }

}



