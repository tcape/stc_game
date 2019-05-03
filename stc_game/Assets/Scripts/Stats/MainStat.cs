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
   
    private void UpdateSubStats()
    {
        foreach (var substat in subStats)
        {
            substat.UpdateBaseValue(this);
        }
    }
}
