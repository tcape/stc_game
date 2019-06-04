using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySaver : Saver
{
    private AbilityManager manager;
    private List<Ability> abilities;

    private void Awake()
    {
        manager = GetComponent<AbilityManager>();
        if (gameObject.CompareTag("Player"))
        {
            uniqueIdentifier = "myAbilities";
        }
        key = SetKey();
    }

    public override void Load()
    {
        var checkList = new List<Ability>();
        
        if (saveData.Load(key, ref checkList))
        {
            abilities = checkList;
        }
    }

    public override void Save()
    {
        manager = GetComponent<AbilityManager>();
        if (manager)
        {
            abilities = manager.myAbilities;
            saveData.Save(key, abilities);
        }
        else
            throw new System.InvalidOperationException();
    }

    protected override string SetKey()
    {
        return uniqueIdentifier;
    }
}
