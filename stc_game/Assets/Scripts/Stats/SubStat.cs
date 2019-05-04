using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SubStat : Stat
{
    [HideInInspector] public MainStat mainStat; // might not need this

    public SubStat(double value) : base(value)
    {
        baseValue = value;
    }

    public void AddModifierFromMainStat(MainStat mainStat, object source)
    {
        AddModifier(new StatModifier(Math.Round(baseValue * mainStat.currentValue / mainStat.stats.nextLevelXP), ModType.Flat, source));
    }

 
}
