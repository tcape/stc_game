using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Space]
    public AudioClip deathSound;
    [Range(0.0f, 1.0f)]
    public float volume;
    private GameObject soundObject;
    private AudioSource source;

    public IEnumerator PlayDeathAudio()
    {
        soundObject = new GameObject("instancedSoundObject");
        soundObject.AddComponent<AudioSource>();
        source = soundObject.GetComponent<AudioSource>();
        source.clip = deathSound;
        source.volume = volume;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        Destroy(soundObject);
    }
}
