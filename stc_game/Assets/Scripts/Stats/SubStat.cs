using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SubStat : Stat
{
    [HideInInspector] public MainStat mainStat;

    public SubStat(double value) : base(value)
    {
        baseValue = value;
    }

    public void UpdateModifierFromMainStat()
    {
        RemoveAllModifiersFromSource(mainStat);
        var stats = PersistentScene.Instance.GameCharacter.Stats;
        var modAmount = Value * mainStat.Value / stats.nextLevelXP;
        AddModifier(new StatModifier(modAmount, ModType.Flat, mainStat));
    }
}
