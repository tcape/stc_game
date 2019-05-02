using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class MainStat
{
    [SerializeField] public double baseValue;
    [SerializeField] public double currentValue;
    [SerializeField] public List<StatModifier> statModifiers;
    
}
