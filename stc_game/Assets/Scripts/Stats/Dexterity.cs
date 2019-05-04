﻿using System;
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
