﻿using System;
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

    public void UpdateBaseValue(MainStat mainStat)
    {
        IncreaseBaseValue(Math.Round(baseValue * mainStat.currentValue / mainStat.stats.nextLevelXP));
    }
}
