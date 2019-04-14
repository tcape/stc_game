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
        statsLoaded = false;
        abilitiesLoaded = false;
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

    private void Start()
    {
        
        //if (!statsLoaded)
        //    characterStats.saver.Load();
        //if (!abilitiesLoaded)
        //    abilityManager.saver.Load();
        //if (!characterStats)
        //    LoadCharacterStats();
        //if (abilityManager.myAbilities.Count.Equals(0))
        //    LoadAbilities();
    }

    public void LoadCharacterStats()
    {
        characterStats.LoadSavedStats(gameCharacter.Stats);
        statsLoaded = true;
    }

    public void LoadAbilities()
    {
        abilityManager.LoadAbilites(gameCharacter.Abilities);
        abilitiesLoaded = true;
    }

    private void OnDestroy()
    {
        //statsLoaded = false;
    }
}
