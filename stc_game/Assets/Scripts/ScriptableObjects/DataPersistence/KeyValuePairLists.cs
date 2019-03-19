using System;
using System.Collections.Generic;


// This nested class is a lighter replacement for
// Dictionaries.  This is required because Dictionaries
// are not serializable.  It has a single generic type
// that represents the type of data to be stored in it.

[Serializable]
public class KeyValuePairLists<T>
{
    public List<string> keys = new List<string>();      // The keys are unique identifiers for each element of data. 
    public List<T> values = new List<T>();              // The values are the elements of data.


    public void Clear()
    {
        keys.Clear();
        values.Clear();
    }


    public void TrySetValue(string key, T value)
    {
        // Find the index of the keys and values based on the given key.
        int index = keys.FindIndex(x => x == key);

        // If the index is positive...
        if (index > -1)
        {
            // ... set the value at that index to the given value.
            values[index] = value;
        }
        else
        {
            // Otherwise add a new key and a new value to the collection.
            keys.Add(key);
            values.Add(value);
        }
    }


    public bool TryGetValue(string key, ref T value)
    {
        // Find the index of the keys and values based on the given key.
        int index = keys.FindIndex(x => x == key);

        // If the index is positive...
        if (index > -1)
        {
            // ... set the reference value to the value at that index and return that the value was found.
            value = values[index];
            return true;
        }

        // Otherwise, return that the value was not found.
        return false;
    }
}