using System;
using System.Collections.Generic;
using Devdog.QuestSystemPro;

[Serializable]
public class GameState
{
    public QuestsContainer QuestsContainer;
    public ChestSaveData ChestSaveData;
    public List<string> Items;
    public List<string> EquippedItems;
    public Stats Stats;
    public bool isDirty = false;

    public GameState()
    {
        isDirty = false;
    }

}

[Serializable]
public class ChestSaveData
{
    public List<string> chestPairListKeys;
    public List<bool> chestPairListValues;

    public ChestSaveData(SaveData saveData)
    {
        chestPairListKeys = saveData.boolKeyValuePairLists.keys;
        chestPairListValues = saveData.boolKeyValuePairLists.values;
    }
}





