using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class MainStat : Stat
{
    [HideInInspector] public HeroStats stats;
    [HideInInspector] public List<SubStat> subStats;

    private void UpdateSubStats()
    {
        foreach (var substat in subStats)
        {
            substat.UpdateBaseValue(this);
        }
    }
}
