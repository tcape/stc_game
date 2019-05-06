using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public double damage;
    public float velocity;


    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!other.gameObject.GetComponent<CharacterStats>().stats.dead)
            {
                //gameObject.SetActive(false);

                
                
                other.gameObject.GetComponent<CharacterStats>().stats.TakeDamage(damage);
                other.gameObject.GetComponent<StateController>().CauseAggro();
                Destroy(gameObject);

                var hit = Instantiate(Resources.Load("FX/FireballHit") as GameObject, other.transform);
                hit.transform.position = other.gameObject.transform.position + new Vector3(0.3f, 1.5f, 0);
                //yield return StartCoroutine(ParticleDuration(hit));
                //Destroy(hit);
            }
           
        }
    }

    private IEnumerator ParticleDuration(GameObject hit)
    {
        yield return new WaitForSeconds(2);
        
        //yield return new WaitUntil( () => hit.GetComponent<ParticleSystem>().time == hit.GetComponent<ParticleSystem>().main.duration);
    }

}
