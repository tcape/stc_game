using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarController : MonoBehaviour
{
    private Stats stats;
    private StateController hero;
    public GameObject parent;
    public GameObject child;

    private void Start()
    {
        stats = GetComponentInParent<CharacterStats>().stats;
        hero = GameObject.FindGameObjectWithTag("Player").GetComponent<StateController>();
        parent = transform.parent.gameObject;
        child = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (hero.target == null)
        {
            child.SetActive(false);
            return;
        }
            
        if (hero.target.Equals(parent.gameObject))
            child.SetActive(true);
        else
            child.SetActive(false);

        if (stats.dead)
            child.SetActive(false);
    }

}
