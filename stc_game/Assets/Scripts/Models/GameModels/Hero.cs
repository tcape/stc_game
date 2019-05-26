using Assets.Scripts.CharacterBehavior.Combat;
using Kryz.CharacterStats.Examples;
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
    public Inventory inventory;
    public List<EquippableItem> equipment;
    private GameObject weapon1;
    private GameObject weapon2;
    private Item mainWeapon;
    private Item offWeapon;
    
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
        inventory = PersistentScene.Instance.inventory;
        equipment = PersistentScene.Instance.equipment;
        weapon1 = GetComponentInChildren<Weapon1>().gameObject;
        mainWeapon = weapon1.GetComponent<GameItem>().item;
        weapon2 = GetComponentInChildren<Weapon2>().gameObject;
        offWeapon = weapon2.GetComponent<GameItem>().item;
    }

    private void UpdateEquipment()
    {
        equipment = PersistentScene.Instance.equipment;

        if (!equipment.Contains((EquippableItem)mainWeapon))
            weapon1.SetActive(false);
        else
            weapon1.SetActive(true);

        if (!equipment.Contains((EquippableItem)offWeapon))
            weapon2.SetActive(false);
        else
            weapon2.SetActive(true);
    }

    private void Update()
    {
        UpdateEquipment();
    }

    public void LoadCharacterStats()
    {
        // characterStats.stats.LoadSavedStats(gameCharacter.Stats);
        characterStats.stats = gameCharacter.Stats;
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
