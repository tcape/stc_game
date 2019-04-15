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
    private bool statsLoaded;
    private bool abilitiesLoaded;


    private void Awake()
    {
        gameCharacter = PersistentScene.Instance.GameCharacter;
        heroName = gameCharacter.Name;
        heroClass = gameCharacter.HeroClass;
        characterStats = GetComponent<CharacterStats>();
        abilityManager = GetComponent<AbilityManager>();
        // inventory = GetComponent<Inventory>();
        // equpment = GetCompnent<Equipment>();
        prefab = gameObject;
        DontDestroyOnLoad(prefab);
    }

    public void LoadCharacterStats()
    {
        characterStats.stats.LoadSavedStats(gameCharacter.Stats);
    }

    public void LoadAbilities()
    {
        abilityManager.LoadAbilites(gameCharacter.Abilities);
    }
}
