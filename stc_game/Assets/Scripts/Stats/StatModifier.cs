using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ModType { Flat, PercentAdd, PercentMult }

public class StatModifier
{
    public ModType type;
    public float value;
    public object source;

    public StatModifier(float value, ModType type)
    {
        this.type = type;
        this.value = value;
        this.source = null;
    }
    public StatModifier(float value, ModType type, object source)
    {
        this.type = type;
        this.value = value;
        this.source = source;
    }



}
