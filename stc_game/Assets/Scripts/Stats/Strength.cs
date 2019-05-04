using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Strength : MainStat
{
    public SubStat attack;
    public SubStat meleeCritPower;
    public SubStat defense;
    public SubStat maxHP;

    public Strength(double value) : base(value)
    {
        baseValue = value;
        Init();
    }

    public void SetSubStats()
    {
        subStats.Add(attack);
        subStats.Add(meleeCritPower);
        subStats.Add(defense);
        subStats.Add(maxHP);
    }

    public void SetMainStat()
    {
        attack.mainStat = this;
        meleeCritPower.mainStat = this;
        defense.mainStat = this;
        maxHP.mainStat = this;
    }

    public void Setup()
    {
        SetSubStats();
        SetMainStat();
        UpdateSubStatModifiers();
    }

    public double Attack()
    {
        return attack.Value;
    }

    public double MeleeCritPower()
    {
        return meleeCritPower.Value;
    }

    public double Defense()
    {
        return defense.Value;
    }

    public double MaxHP()
    {
        return maxHP.Value;
    }

    public void Init()
    {
        attack = new SubStat(0);
        meleeCritPower = new SubStat(0);
        defense = new SubStat(0);
        maxHP = new SubStat(0);
    }
}
