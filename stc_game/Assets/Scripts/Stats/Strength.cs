using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Strength : MainStat
{
    [SerializeField] public SubStat[] subStats;

    public Strength()
    {
        subStats = new SubStat[4];
        subStats[0] = new SubStat("Attack");
        subStats[1] = new SubStat("MeleeCritPower");
        subStats[2] = new SubStat("Defense");
        subStats[3] = new SubStat("MaxHP");
    }

}
