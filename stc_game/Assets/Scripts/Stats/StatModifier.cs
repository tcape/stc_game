using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ModType { Flat, PercentAdd, PercentMult }

public class StatModifier
{
    public ModType Type;
    public double Value;
    public int Order;
    public object Source;

    public StatModifier(double value, ModType type, int order)
    {
        Type = type;
        Value = value;
        Order = order;
        Source = null;
    }

    public StatModifier(double value, ModType type)
    {
        Type = type;
        Value = value;
        Order = (int)type;
        Source = null;
    }

    public StatModifier(double value, ModType type, object source)
    {
        Type = type;
        Value = value;
        Order = (int)type;
        Source = source;
    }
}
