using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class Stat
{
    public double baseValue;
    public double currentValue;
    protected bool isDirty = true;
    protected double lastBaseValue;
    public virtual double Value
    {
        get
        {
            Refresh();
            return currentValue;
        }
    }

    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public Stat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public Stat(double value) : this()
    {
        baseValue = value;
        Refresh();
    }

    public virtual void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        Refresh();
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            Refresh();
            isDirty = true;
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        int numRemovals = statModifiers.RemoveAll(mod => mod.Source == source);

        if (numRemovals > 0)
        {
            isDirty = true;
            Refresh();
            return true;
        }
        return false;
    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0; //if (a.Order == b.Order)
    }

    protected virtual double CalculateFinalValue()
    {
        double finalValue = baseValue;
        double sumPercentAdd = 0;

        statModifiers.Sort(CompareModifierOrder);

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.Type == ModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == ModType.PercentAdd)
            {
                sumPercentAdd += mod.Value;

                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != ModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (mod.Type == ModType.PercentMult)
            {
                finalValue *= 1 + mod.Value;
            }
        }

        return Math.Round(finalValue, 4);
    }

    public void IncreaseBaseValue(double amount)
    {
        baseValue += amount;
        Refresh();
    }

    public void Refresh()
    {
        if (isDirty || lastBaseValue != baseValue)
        {
            lastBaseValue = baseValue;
            currentValue = CalculateFinalValue();
            isDirty = false;
        }
    }
}
