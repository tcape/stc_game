﻿using Devdog.QuestSystemPro;
using Devdog.QuestSystemPro.Dialogue;
using Devdog.QuestSystemPro.Dialogue.UI;
using Devdog.QuestSystemPro.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentScene : MonoBehaviour
{
    public static PersistentScene Instance;
    public User user;
    public UserService userService = UserService.Instance;
    public GameCharacter GameCharacter;
    public QuestWindowUI questWindowUI;
    public DialogueUI dialogueUI;
    private HUDController hud;
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
    }

    private void Start()
    {
        hud = FindObjectOfType<HUDController>();
        hud.gameObject.SetActive(false);
        questWindowUI = GameObject.FindObjectOfType<QuestWindowUI>();
        QuestManager.instance.questWindowUI = PersistentScene.Instance.questWindowUI;

        dialogueUI = GameObject.FindObjectOfType<DialogueUI>();
        DialogueManager.instance.dialogueUI = PersistentScene.Instance.dialogueUI;

        userService.LoadUserCallback += HandleLoadUserCallback;
        userService.GetUser(AuthService.Instance.authUser.sub);

        // Substitue GameCharacter to be replaced by GameCharacter data from database
        // This is just for testing
        GameCharacter = new GameCharacter(
                                            "WarriorTest",
                                            HeroClass.Warrior,
                                            new Stats()
                                            {
                                                level = 1,
                                                XP = 0,
                                                gold = 0,
                                                maxHP = 500,
                                                maxAP = 150,
                                                currentHP = 500,
                                                currentAP = 150,
                                                strength = 10,
                                                intellect = 5,
                                                dexterity = 7,
                                                attack = 15,
                                                meleeCritPower = 2,
                                                defense = 9,
                                                abilityAttack = 10,
                                                abilityCritRate = 0.25,
                                                abilityCritPower = 2,
                                                meleeCritRate = 0.25,
                                                dodgeRate = 0.15,
                                                movementSpeed = 7,
                                                dead = false,
                                                nextLevelXP = 100
                                            }
                                            ,
                                            new List<string>()
                                            {
                                               "RegenerateAP",
                                               "IronSkin",
                                               "Savagry",
                                               "Cleave",
                                               "Sprint"
                                            }
                                            );

                                            
    }

    public void SaveGameCharacterStats(Stats saveStats)
    {
        GameCharacter.Stats = saveStats;
    }

    private void HandleLoadUserCallback()
    {
        user = userService.user;
        StartCoroutine(LoadGameScene());
    }

    private IEnumerator LoadGameScene()
    {
        yield return StartCoroutine(SceneController.Instance.LoadFirstScene());
        hud.FindPlayerObject();
        hud.gameObject.SetActive(true);
    }
}
