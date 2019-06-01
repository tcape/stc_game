using Assets.Scripts.CharacterBehavior.BaseClasses;
using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/AbilityAction/SoundAction/PlaySound")]
public class SoundAction : AbilityAction
{
    public AudioClip sound;
    [Range(0.0f, 1.0f)]
    public float volume;
    private GameObject soundObject;
    private AudioSource source;

    void Start()
    {
        
    }

    public override void Act(AbilityManager manager)
    {
        soundObject = new GameObject("instancedSoundObject");
        soundObject.AddComponent<AudioSource>();
        source = soundObject.GetComponent<AudioSource>();
        source.clip = sound;
        source.volume = volume;
        source.Play();
    }

    public override void RemoveEffect(AbilityManager manager)
    {
        Destroy(soundObject);
    }

    public override void ResetEffectTotal()
    {
        
    }

    public override void UpdateEffectTotal()
    {
        
    }
}
