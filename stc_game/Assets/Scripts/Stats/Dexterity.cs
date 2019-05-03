using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dexterity : MainStat
{
    [SerializeField] SubStat meleeCritRate;
    [SerializeField] SubStat dodgeRate;
    [SerializeField] SubStat movementSpeed;

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
}
