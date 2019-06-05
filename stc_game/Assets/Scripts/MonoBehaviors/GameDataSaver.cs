using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using Devdog.QuestSystemPro;
using System.Collections.Generic;
using UnityEngine;
using Kryz.CharacterStats.Examples;
using System.Linq;

public class GameDataSaver : Saver
{
    private void Awake()
    {
        
    }

    public override void Load()
    {
        if (PersistentScene.Instance.User.GetActiveCharacter().GameState.isDirty)
        {
            PersistentScene.Instance.LoadGameData();
        }
    }

    public override void Save()
    {
        var dbItems = new List<string>();
        var dbEquipment = new List<string>();

        var inventoryManager = PersistentScene.Instance.inventoryManager;
        var inventory = PersistentScene.Instance.inventory;
        var equipment = PersistentScene.Instance.equipment;
        var items = PersistentScene.Instance.inventory.items;
        var questProgress = QuestManager.instance.GetQuestStates();
        var gameState = UserService.Instance.User.GetActiveCharacter().GameState;


        // save items
        foreach (var item in items)
        {
            dbItems.Add(item.Id);
        }
        gameState.Items = dbItems;

        // clear inventory
        items.Clear();

        // save equipment
        foreach (var item in equipment)
        {
            dbEquipment.Add(item.Id);

        }
        gameState.EquippedItems = dbEquipment;

        foreach(var id in dbEquipment)
        {
            inventoryManager.Unequip(equipment.Where(e => e.Id.Equals(id)).SingleOrDefault());
        }

        items.Clear();

        // save stats
        gameState.Stats = PersistentScene.Instance.GameCharacter.Stats;

        // save quest progress
        // gameState.QuestsContainer = questProgress;

        gameState.isDirty = true;
        // save to User Database
        UserApi.Instance.CreateOrUpdate(UserService.Instance.User);
    }

    protected override string SetKey()
    {
        throw new System.NotImplementedException();
    }
}
