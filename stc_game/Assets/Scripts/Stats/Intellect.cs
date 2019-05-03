using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Intellect : MainStat
{
    [SerializeField] SubStat abilityAttack;
    [SerializeField] SubStat abilityCritPower;
    [SerializeField] SubStat abilityCritRate;
    [SerializeField] SubStat maxAP;

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
}
