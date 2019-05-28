using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestStateSaver : Saver
{
    private ChestOpener chest;
    private bool opened;

    private void Awake()
    {
        chest = GetComponent<ChestOpener>();
        opened = chest.opened;
        key = SetKey();
        saveData = Resources.Load<SaveData>("SaveData/PlayerSaveData");
    }

    public override void Load()
    {
        bool check = false ;
        if (saveData.Load(key, ref check))
        {
            opened = check;
            GetComponent<ChestOpener>().SetOpened(opened);
        }
    }

    public override void Save()
    {
        chest = GetComponent<ChestOpener>();
        if (chest)
        {
            opened = chest.opened;
            saveData.Save(key, opened);
        }
    }

    protected override string SetKey()
    {
        return uniqueIdentifier;
    }
}
