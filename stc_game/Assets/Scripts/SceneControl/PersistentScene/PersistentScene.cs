using Devdog.QuestSystemPro;
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

    private void Start()
    {
        hud = FindObjectOfType<HUDController>();
        hud.gameObject.SetActive(false);
        questWindowUI = FindObjectOfType<QuestWindowUI>();
        QuestManager.instance.questWindowUI = questWindowUI;
        reviveController = FindObjectOfType<ReviveController>();

        dialogueUI = FindObjectOfType<DialogueUI>();
        DialogueManager.instance.dialogueUI = dialogueUI;

        userService.LoadUserCallback += HandleLoadUserCallback;

        if (UserService.Instance.user._id != "")
        {
            userService.GetUser(AuthService.Instance.authUser.sub);
        }
        else // for now so we can start from persistent scene without having to login
        {
            StartCoroutine(LoadGameScene());
        }

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
