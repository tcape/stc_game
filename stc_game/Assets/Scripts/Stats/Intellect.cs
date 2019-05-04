using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Intellect : MainStat
{
    public SubStat abilityAttack;
    public SubStat abilityCritPower;
    public SubStat abilityCritRate;
    public SubStat maxAP;

    public Intellect(double value) : base(value)
    {
        baseValue = value;
        Init();
    }

    public void Init()
    {
        abilityAttack = new SubStat(0);
        abilityCritPower = new SubStat(0);
        abilityCritRate = new SubStat(0);
        maxAP = new SubStat(0);
    }

    private void SetSubStats()
    {
        subStats.Add(abilityAttack);
        subStats.Add(abilityCritPower);
        subStats.Add(abilityCritRate);
        subStats.Add(maxAP);
    }

    private void SetMainStat()
    {
        abilityAttack.mainStat = this;
        abilityCritPower.mainStat = this;
        abilityCritRate.mainStat = this;
        maxAP.mainStat = this;
    }

    public void Setup()
    {
        SetSubStats();
        SetMainStat();
    }

    public double AbilityAttack()
    {
        return abilityAttack.Value;
    }

    public double AbilityCritPower()
    {
        return abilityCritPower.Value;
    }

    public double AbilityCritRate()
    {
        return abilityCritRate.Value;
    }

    public double MaxAP()
    {
        return maxAP.Value;
    }
}

