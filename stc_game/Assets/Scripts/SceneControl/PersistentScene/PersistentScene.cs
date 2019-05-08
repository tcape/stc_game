﻿using Devdog.QuestSystemPro;
using Devdog.QuestSystemPro.Dialogue;
using Devdog.QuestSystemPro.Dialogue.UI;
using Devdog.QuestSystemPro.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersistentScene : MonoBehaviour
{
    public static PersistentScene Instance;
    public User user;
    public UserService userService = UserService.Instance;
    public GameCharacter GameCharacter;
    public ReviveController reviveController;
    public ActionBarController actionBar;
    public QuestWindowUI questWindowUI;
    public DialogueUI dialogueUI;
    public HUDController hud;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Substitue GameCharacter to be replaced by GameCharacter data from database
        // This is just for testing
        // moved this from Start to Awake for now so abilities are can be loaded

        GameCharacter = new GameCharacter(
                                           "MageTest",
                                           HeroClass.Mage,
                                           new Stats()
                                           {
                                               level = 1,
                                               XP = 0,
                                               gold = 0,
                                               strength = new Strength(5)
                                               {
                                                   maxHP = new SubStat(500),
                                                   attack = new SubStat(6),
                                                   meleeCritPower = new SubStat(1.5),
                                                   defense = new SubStat(5)
                                               },
                                               intellect = new Intellect(10)
                                               {
                                                   maxAP = new SubStat(180),
                                                   abilityAttack = new SubStat(15),
                                                   abilityCritPower = new SubStat(2),
                                                   abilityCritRate = new SubStat(0.25)
                                               },
                                               dexterity = new Dexterity(5)
                                               {
                                                   dodgeRate = new SubStat(0.15),
                                                   meleeCritRate = new SubStat(0.25),
                                                   movementSpeed = new SubStat(5)
                                               },
                                               currentHP = 500,
                                               currentAP = 180,
                                               dead = false,
                                               nextLevelXP = 100
                                           }
                                           ,
                                           new List<string>()
                                           {
                                               "RegenerateAP",
                                               "Fireball",
                                               "Curse",
                                               "Slow",
                                               "Teleport"
                                           }
                                           );

        GameCharacter.Stats.Setup();
      
    }

    private void Start()
    {
        // load local resources
        hud = FindObjectOfType<HUDController>();
        hud.gameObject.SetActive(false);
        questWindowUI = FindObjectOfType<QuestWindowUI>();
        QuestManager.instance.questWindowUI = questWindowUI;
        reviveController = FindObjectOfType<ReviveController>();
        dialogueUI = FindObjectOfType<DialogueUI>();
        DialogueManager.instance.dialogueUI = dialogueUI;
        StartCoroutine(LoadGameScene());
    }

    public void SaveGameCharacterStats(Stats saveStats)
    {
        GameCharacter.Stats = saveStats;
    }

    private IEnumerator LoadGameScene()
    {
        yield return StartCoroutine(SceneController.Instance.LoadFirstScene());
        hud.FindPlayerObject();
        hud.gameObject.SetActive(true);
    }
}
