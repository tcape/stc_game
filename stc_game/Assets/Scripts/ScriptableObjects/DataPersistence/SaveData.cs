﻿using System;
using UnityEngine;
using System.Collections.Generic;

// Instance of this class can be created as assets.
// Each instance contains collections of data from
// the Saver monobehaviours they have been referenced
// by.  Since assets exist outside of the scene, the
// data will persist ready to be reloaded next time
// the scene is loaded.  Note that these assets
// DO NOT persist between loads of a build and can
// therefore NOT be used for saving the gamestate to
// disk.
[CreateAssetMenu]
public class SaveData : ResettableScriptableObject
{
    // These are collections for various different data types.
    public KeyValuePairLists<bool> boolKeyValuePairLists = new KeyValuePairLists<bool> ();
    public KeyValuePairLists<int> intKeyValuePairLists = new KeyValuePairLists<int>();
    public KeyValuePairLists<string> stringKeyValuePairLists = new KeyValuePairLists<string>();
    public KeyValuePairLists<CharacterStats> characterStatsKeyValuePairLists = new KeyValuePairLists<CharacterStats>();
    //public KeyValuePairLists<Inventory> inventoryKeyValuePairLists = new KeyValuePairLists<Inventory>();
    public KeyValuePairLists<Vector3> vector3KeyValuePairLists = new KeyValuePairLists<Vector3>();
    public KeyValuePairLists<Quaternion> quaternionKeyValuePairLists = new KeyValuePairLists<Quaternion>();


    public override void Reset ()
    {
        boolKeyValuePairLists.Clear ();
        intKeyValuePairLists.Clear ();
        stringKeyValuePairLists.Clear ();
        vector3KeyValuePairLists.Clear ();
        quaternionKeyValuePairLists.Clear ();
    }


    // This is the generic version of the Save function which takes a
    // collection and value of the same type and then tries to set a value.
    private void Save<T>(KeyValuePairLists<T> lists, string key, T value)
    {
        lists.TrySetValue(key, value);
    }


    // This is similar to the generic Save function, it tries to get a value.
    private bool Load<T>(KeyValuePairLists<T> lists, string key, ref T value)
    {
        return lists.TryGetValue(key, ref value);
    }


    // This is a public overload for the Save function that specifically
    // chooses the generic type and calls the generic version.
    public void Save (string key, bool value)
    {
        Save(boolKeyValuePairLists, key, value);
    }

    public void Save (string key, int value)
    {
        Save(intKeyValuePairLists, key, value);
    }

    public void Save (string key, string value)
    {
        Save(stringKeyValuePairLists, key, value);
    }

    public void Save(string key, CharacterStats value)
    {
        Save(characterStatsKeyValuePairLists, key, value);
    }

    public void Save (string key, Vector3 value)
    {
        Save(vector3KeyValuePairLists, key, value);
    }


    public void Save (string key, Quaternion value)
    {
        Save(quaternionKeyValuePairLists, key, value);
    }


    // This works the same as the public Save overloads except
    // it calls the generic Load function.
    public bool Load (string key, ref bool value)
    {
        return Load(boolKeyValuePairLists, key, ref value);
    }


    public bool Load (string key, ref int value)
    {
        return Load (intKeyValuePairLists, key, ref value);
    }


    public bool Load (string key, ref string value)
    {
        return Load (stringKeyValuePairLists, key, ref value);
    }

    public bool Load(string key, ref CharacterStats value)
    {
        return Load(characterStatsKeyValuePairLists, key, ref value);
    }

    public bool Load (string key, ref Vector3 value)
    {
        return Load(vector3KeyValuePairLists, key, ref value);
    }


    public bool Load (string key, ref Quaternion value)
    {
        return Load (quaternionKeyValuePairLists, key, ref value);
    }
}
