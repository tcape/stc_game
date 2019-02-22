using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public Ability[] abilities;
    [HideInInspector] public Animator animator;
    [HideInInspector] public CharacterStats stats;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<CharacterStats>();
        abilities = GetComponent<AbilityController>().abilities;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
