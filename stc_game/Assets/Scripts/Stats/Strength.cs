using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Strength : MainStat
{
    [SerializeField] public SubStat attack;
    [SerializeField] public SubStat meleeCritPower;
    [SerializeField] public SubStat defense;
    [SerializeField] public SubStat maxHP;

    public Strength(double value) : base(value)
    {
        baseValue = value;
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
}
