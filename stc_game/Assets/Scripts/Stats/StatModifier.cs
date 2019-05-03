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
        this.Type = type;
        this.Value = value;
        this.Order = order;
        this.Source = null;
    }

    public StatModifier(double value, ModType type)
    {
        this.Type = type;
        this.Value = value;
        this.Order = (int)type;
        this.Source = null;
    }

    public StatModifier(double value, ModType type, object source)
    {
        this.Type = type;
        this.Value = value;
        this.Order = (int)type;
        this.Source = source;
    }

}
