using Assets.Scripts.CharacterBehavior.Combat;
using Kryz.CharacterStats.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private Weapon1[] weapon1;
    private Weapon2[] weapon2;
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
        weapon1 = GetComponentsInChildren<Weapon1>(true);
        mainWeapon = equipment.Where(e => e.EquipmentType.Equals(EquipmentType.Weapon1)).SingleOrDefault();
        weapon2 = GetComponentsInChildren<Weapon2>(true);
        offWeapon = equipment.Where(e => e.EquipmentType.Equals(EquipmentType.Weapon2)).SingleOrDefault();
    }

    private void Start()
    {
        UpdateEquipment();
    }

    private void UpdateEquipment()
    {
        equipment = PersistentScene.Instance.equipment;

        mainWeapon = equipment.Where(e => e.EquipmentType.Equals(EquipmentType.Weapon1)).SingleOrDefault();
        offWeapon = equipment.Where(e => e.EquipmentType.Equals(EquipmentType.Weapon2)).SingleOrDefault();
        
        if (!mainWeapon)
        {
            foreach (var weapon in weapon1)
            {
                weapon.gameObject.SetActive(false);
            }
        }
        else 
        {
            foreach(var weapon in weapon1)
            {
                if (weapon.gameObject.GetComponent<GameItem>().item.Equals(mainWeapon))
                    weapon.gameObject.SetActive(true);
                else weapon.gameObject.SetActive(false);
            }
        }

        if (!offWeapon)
        {
            foreach (var weapon in weapon2)
            {
                weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (var weapon in weapon2)
            {
                if (weapon.gameObject.GetComponent<GameItem>().item.Equals(mainWeapon))
                    weapon.gameObject.SetActive(true);
                else weapon.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        //UpdateEquipment();
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

    public void OnEquipmentChange(List<EquippableItem> equippedItems)
    {
        equipment = equippedItems;

        mainWeapon = equipment.Where(e => e.EquipmentType.Equals(EquipmentType.Weapon1)).SingleOrDefault();
        offWeapon = equipment.Where(e => e.EquipmentType.Equals(EquipmentType.Weapon2)).SingleOrDefault();

        if (!mainWeapon)
        {
            foreach (var weapon in weapon1)
            {
                weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (var weapon in weapon1)
            {
                if (weapon.gameObject.GetComponent<GameItem>().item.Equals(mainWeapon))
                    weapon.gameObject.SetActive(true);
                else weapon.gameObject.SetActive(false);
            }
        }

        if (!offWeapon)
        {
            foreach (var weapon in weapon2)
            {
                weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (var weapon in weapon2)
            {
                if (weapon.gameObject.GetComponent<GameItem>().item.Equals(offWeapon))
                    weapon.gameObject.SetActive(true);
                else weapon.gameObject.SetActive(false);
            }
        }
    }
}
