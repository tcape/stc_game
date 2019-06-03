using Devdog.General;
using Devdog.QuestSystemPro;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.CharacterBehavior.Drops

{
    public class Gold : MonoBehaviour
    {
        public double amount;
        [Space]
        public AudioClip sound;
        [Range(0.0f, 1.0f)]
        public float volume;
        public ParticleSystem pickupEffect;
        private GameObject soundObject;
        private AudioSource source;

        public void SetAmount(double value)
        {
            amount = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && other.isTrigger)
            {
                other.GetComponent<CharacterStats>().stats.GainGold(amount);
                Debug.Log("Gold given to hero " + amount.ToString());
                // Call OnTriggerUsed here for gold quest instead of triggering with range
                GetComponent<SetQuestProgressOnTriggerObjectGold>().OnTriggerUsed(other.GetComponent<Player>());
                StartCoroutine(PlayPickupAudio());
                var instance = Instantiate(pickupEffect, transform.position, Quaternion.identity);
                instance.transform.Rotate(-90, 0, 0);
                Destroy(gameObject);
            }
        }

        private IEnumerator PlayPickupAudio()
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
}
