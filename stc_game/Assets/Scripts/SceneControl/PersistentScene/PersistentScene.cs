using Devdog.QuestSystemPro;
using Devdog.QuestSystemPro.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentScene : MonoBehaviour
{
    public static PersistentScene Instance;
    public GameCharacter GameCharacter;

    public QuestWindowUI questWindowUI;
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
        questWindowUI = GameObject.FindObjectOfType<QuestWindowUI>();
        QuestManager.instance.questWindowUI = PersistentScene.Instance.questWindowUI;
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
                                            //Resources.Load<StatsPreset>("StatsPresets/WarriorBaseStats"),
                                            new List<string>()
                                            {
                                               "RegenerateAP",
                                               "IronSkin"
                                            },
                                            //Resources.Load<QuestDatabase>(""),
                                            "Prefabs/WarriorPrefab"
                                            );

                                            
    }
}
