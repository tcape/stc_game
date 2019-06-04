using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public double damage;
    public CharacterStats heroStats;
    [Space]
    public AudioClip sound;
    [Range(0.0f, 1.0f)]
    public float volume = .5f;
    private GameObject soundObject;
    private AudioSource source;
    private float startTime;
    private float duration;


    private void Start()
    {
        heroStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        damage = 1;
        startTime = Time.time;
        duration = 10f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            if (!other.gameObject.GetComponent<CharacterStats>().stats.dead)
            {
                other.gameObject.GetComponent<CharacterStats>().stats.TakeAbilityDamage(heroStats, damage);
                if (!other.gameObject.GetComponent<CharacterStats>().stats.dead)
                    other.gameObject.GetComponent<StateController>().CauseAggro();
                StartCoroutine(PlayAudio());
                Destroy(gameObject);

                var hit = Instantiate(Resources.Load("FX/FireballHit") as GameObject, other.transform);
                hit.transform.position = other.gameObject.transform.position + new Vector3(0f, 1.5f, 0);
            }
           
        }
    }

    private void Update()
    {
        if (startTime < Time.time - duration)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator PlayAudio()
    {
        soundObject = new GameObject("instancedSoundObject");
        soundObject.AddComponent<AudioSource>();
        source = soundObject.GetComponent<AudioSource>();
        source.clip = sound;
        source.volume = volume;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        Destroy(soundObject);
    }

}
