using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dexterity : MainStat
{
    public SubStat meleeCritRate;
    public SubStat dodgeRate;
    public SubStat movementSpeed;

    public Dexterity(double value) : base(value)
    {
        baseValue = value;
        Init();
    }

    public void Init()
    {
        meleeCritRate = new SubStat(0);
        dodgeRate = new SubStat(0);
        movementSpeed = new SubStat(0);
    }

    private void SetSubStats()
    {
        subStats.Add(meleeCritRate);
        subStats.Add(dodgeRate);
        subStats.Add(movementSpeed);
    }

    private void SetMainStat()
    {
        meleeCritRate.mainStat = this;
        dodgeRate.mainStat = this;
        movementSpeed.mainStat = this;
    }

    public void Setup()
    {
        SetSubStats();
        SetMainStat();
        UpdateSubStatModifiers();
    }

    public double MeleeCritRate()
    {
        return meleeCritRate.Value;
    }

    public double DodgeRate()
    {
        return dodgeRate.Value;
    }

    public double MovementSpeed()
    {
        return movementSpeed.Value;
    }
}
