using Assets.Scripts.CharacterBehavior.BaseClasses;
using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Ability/AbilityAction/ParticleAction")]
public class InstantParticle : AbilityAction
{
    public ParticleSystem particlePrefab;
    private ParticleSystem prefabInstance;
    private bool notStarted = true;
    private void OnEnable()
    {
        notStarted = true;
        lastTick = 0;
        effectTotal = 0;
    }

    private void OnDestroy()
    {
        Destroy(prefabInstance);
    }

    public override void Act(AbilityManager manager)
    {
        //spawn particle effect
        if (notStarted)
        {
            prefabInstance = Instantiate(particlePrefab);
            prefabInstance.transform.position = manager.transform.position;
            prefabInstance.transform.parent = manager.transform;
            notStarted = false;
        }
    }

    public override void RemoveEffect(AbilityManager manager)
    {
        lastTick = 0;
        effectTotal = 0;
        //delete particle effect
        Destroy(prefabInstance);
        notStarted = true;
    }

    public override void ResetEffectTotal()
    {
        
    }

    public override void UpdateEffectTotal()
    {
        
    }
}
