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
    public AbilityManager abilityManager;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public Rigidbody rigidbody;
    public CapsuleCollider physicsCollider;
    public StateController stateController;
    public SpawnManager spawner;
    //public Inventory inventory;
    //public Equipment equipment;
    
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
        spawner = GetComponent<SpawnManager>();
        // inventory = GetComponent<Inventory>();
        // equipment = GetCompnent<Equipment>();
    }

    public void LoadCharacterStats()
    {
        characterStats.stats.LoadSavedStats(gameCharacter.Stats);
    }

    public void LoadAbilities()
    {
        abilityManager.LoadAbilites(gameCharacter.Abilities);
    }

    public void SetHeroTransform(string spawnPositionName)
    {
        var spawnPoint = spawner.GetSpawnPoint(spawnPositionName);
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }

    public void SetReviveComponents()
    {
        characterStats.stats.dead = false;
        animator.SetBool("Dead", false);
        navMeshAgent.enabled = true;
        rigidbody.isKinematic = false;
        physicsCollider.enabled = true;
        stateController.currentState = stateController.startState;
    }
}
