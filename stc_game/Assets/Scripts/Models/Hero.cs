using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hero : MonoBehaviour
{
    private GameCharacter gameCharacter;
    public string heroName;
    public HeroClass heroClass;
    public CharacterStats characterStats;
    //public Inventory inventory;
    //public Equipment equipment;
    public AbilityManager abilityManager;
    public GameObject prefab;


    private void Awake()
    {
        gameCharacter = FindObjectOfType<GameCharacter>();
        if (!gameCharacter)
        {
            Debug.Log("Game character not loaded.");
            return;
        }
        heroName = gameCharacter.Name;
        heroClass = gameCharacter.HeroClass;
        characterStats = GetComponent<CharacterStats>();
        // inventory = GetComponent<Inventory>();
        // equpment = GetCompnent<Equipment>();
        abilityManager = GetComponent<AbilityManager>();
        prefab = gameObject;
    }
}
