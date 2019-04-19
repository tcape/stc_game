using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hero : MonoBehaviour
{
    private GameCharacter gameCharacter;
    public string heroName;
    public HeroClass heroClass;
    public CharacterStats characterStats;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public Rigidbody rigidbody;
    public CapsuleCollider physicsCollider;
    public StateController stateController;
    //public Inventory inventory;
    //public Equipment equipment;
    public AbilityManager abilityManager;
    //public GameObject prefab;

    private void Awake()
    {
        gameCharacter = PersistentScene.Instance.GameCharacter;
        heroName = gameCharacter.Name;
        heroClass = gameCharacter.HeroClass;
        characterStats = GetComponent<CharacterStats>();
        abilityManager = GetComponent<AbilityManager>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        physicsCollider = GetComponent<CapsuleCollider>();
        stateController = GetComponent<StateController>();
        // inventory = GetComponent<Inventory>();
        // equipment = GetCompnent<Equipment>();
        //prefab = gameObject;
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
