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
    }
   
    private void AddSubStatModifiers(object source)
    {
        foreach (var substat in subStats)
        {
            substat.AddModifierFromMainStat(this, source);
        }
    }

    public void AddStatModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);

        AddSubStatModifiers(mod.Source);

        Refresh();
    }

    public bool RemoveStatModifiersFromSource(object source)
    {

        int numRemovals = statModifiers.RemoveAll(mod => mod.Source == source);

        if (numRemovals > 0)
        {
            isDirty = true;
            foreach (var substat in subStats)
            {
                RemoveAllModifiersFromSource(source);
            }
            Refresh();
            return true;
        }
        return false;
    }



}
