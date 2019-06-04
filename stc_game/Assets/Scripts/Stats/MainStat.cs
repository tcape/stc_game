using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class MainStat : Stat
{
    [HideInInspector] public Stats stats;
    [HideInInspector] public List<SubStat> subStats;

    public MainStat(double value) : base(value)
    {
        baseValue = value;
        subStats = new List<SubStat>();
    }
   
    public void UpdateSubStatModifiers()
    {
        foreach (var substat in subStats)
        {
            substat.UpdateModifierFromMainStat();
        }
    }

    public void AddStatModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);

        UpdateSubStatModifiers();

        Refresh();
    }

    public bool RemoveStatModifiersFromSource(object source)
    {

        int numRemovals = statModifiers.RemoveAll(mod => mod.Source == source);

        if (numRemovals > 0)
        {
            isDirty = true;
            UpdateSubStatModifiers();
            Refresh();
            return true;
        }
        return false;
    }

}
