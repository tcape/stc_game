using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using Devdog.QuestSystemPro;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSaver : Saver
{
    private void Awake()
    {
        
    }

    public override void Load()
    {
        // Load is implemented at game launch in persistent scene
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
            inventory.RemoveItem(item);
        }
        gameState.Items = dbItems;


        // save equipment
        foreach (var item in equipment)
        {
            dbEquipment.Add(item.Id);
            inventoryManager.Unequip(item);
            inventory.RemoveItem(item);
        }
        gameState.EquippedItems = dbEquipment;

        // save stats
        gameState.Stats = PersistentScene.Instance.GameCharacter.Stats;

        // save quest progress
        gameState.QuestsContainer = questProgress;

        gameState.isDirty = true;
        // save to User Database
        UserApi.Instance.CreateOrUpdate(UserService.Instance.User);
    }

    protected override string SetKey()
    {
        throw new System.NotImplementedException();
    }
}
