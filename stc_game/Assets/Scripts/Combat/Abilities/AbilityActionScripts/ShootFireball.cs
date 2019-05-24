using Assets.Scripts.CharacterBehavior.BaseClasses;
using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityAction/ShootFireball")]

public class ShootFireball : AbilityAction
{
    public override void Act(AbilityManager manager)
    {
        var fireball = Instantiate(Resources.Load("FX/Fireball")) as GameObject;
        fireball.transform.position = manager.gameObject.transform.position + new Vector3(0, 1.5f, 0);
        var values = fireball.GetComponent<Fireball>();
        values.damage = amount;
        var rb = fireball.GetComponent<Rigidbody>();
        rb.velocity = (target.transform.position - fireball.transform.position).normalized * 15;
    }

    public override void RemoveEffect(AbilityManager manager)
    {
        return;
    }

    public override void ResetEffectTotal()
    {
        return;
    }

    public override void UpdateEffectTotal()
    {
        return;
    }
}
