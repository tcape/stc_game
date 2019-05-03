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

    private void SetSubStats()
    {
        subStats.Add(attack);
        subStats.Add(meleeCritPower);
        subStats.Add(defense);
        subStats.Add(maxHP);
    }

    private void SetMainStat()
    {
        attack.mainStat = this;
        meleeCritPower.mainStat = this;
        defense.mainStat = this;
        maxHP.mainStat = this;
    }

}
