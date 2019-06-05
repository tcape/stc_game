using Devdog.QuestSystemPro;
using Devdog.QuestSystemPro.Dialogue;
using Devdog.QuestSystemPro.Dialogue.UI;
using Devdog.QuestSystemPro.UI;
using Kryz.CharacterStats.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersistentScene : MonoBehaviour
{
    public static PersistentScene Instance = null;
    public User User;
    public GameCharacter GameCharacter;
    public ReviveController reviveController;
    public ActionBarController actionBar;
    public QuestWindowUI questWindowUI;
    public GameDataSaver gameDataSaver;
    public bool firstGameLoad;
    public DialogueUI dialogueUI;
    public HUDController hud;
    public Button logoutButton;
    public Button exitButton;
    public LogoutCanvas logoutCanvas;
    public InventoryManager inventoryManager;
    public Inventory inventory;
    public List<EquippableItem> equipment;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        User = UserService.Instance.User;
        GameCharacter = new GameCharacter(User.GetActiveCharacter().Name, User.GetActiveCharacter().HeroClass);
    }

    private void Start()
    {
        // load local resources 
        // (moving all logic from start function to this first time function)
        firstGameLoad = true;
        gameDataSaver = GetComponent<GameDataSaver>();
        logoutButton.onClick.AddListener(onLogout);
        exitButton.onClick.AddListener(onExit);
        logoutCanvas = FindObjectOfType<LogoutCanvas>();
        questWindowUI = FindObjectOfType<QuestWindowUI>();
        reviveController = FindObjectOfType<ReviveController>();
        hud = FindObjectOfType<HUDController>();
        dialogueUI = FindObjectOfType<DialogueUI>();
        actionBar = GetComponentInChildren<ActionBarController>();
        inventoryManager = gameObject.GetComponentInChildren<InventoryManager>(true);
        inventory = inventoryManager.inventory;
        equipment = gameObject.GetComponentInChildren<EquipmentPanel>(true).equipment;

        // set up component
        logoutCanvas.gameObject.SetActive(false);
        hud.gameObject.SetActive(false);
        QuestManager.instance.questWindowUI = questWindowUI;
        DialogueManager.instance.dialogueUI = dialogueUI;
        StartCoroutine(LoadGameScene());
        exitButton.enabled = false;
        logoutButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);

        if (User.GetActiveCharacter().GameState.isDirty)
        {
            LoadGameData();
            User.GetActiveCharacter().GameState.isDirty = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMenu();
        }
    }



    public void ToggleMenu()
    {
        if (exitButton.enabled)
        {
            exitButton.enabled = false;
            logoutButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(false);
        }
        else
        {
            exitButton.enabled = true;
            logoutButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
        }
    }

    public void SaveGameCharacterStats(Stats saveStats)
    {
        GameCharacter.Stats = saveStats;
    }

    public void LoadGameData()
    {
        var stats = UserService.Instance.User.GetActiveCharacter().GameState.Stats;
        // Load Game Stats
       
        GameCharacter.Stats = GameCharacter.GetStatsFromData(UserService.Instance.User.GetActiveCharacter().GameState.Stats);
        GameCharacter.Stats.Setup();

        Debug.Log(GameCharacter.Stats);
        // Load Equipment from User into Game Character Equipment
        foreach (string equipment in User.GetActiveCharacter().GameState.EquippedItems)
        {
            var resource = Resources.Load<EquippableItem>("Items/" + equipment);
            inventory.AddItem(resource);
            inventoryManager.Equip(resource);
        }

        // Load Items from User into Game Character Inventory
        foreach (string item in User.GetActiveCharacter().GameState.Items)
        {
            inventory.AddItem(Resources.Load<EquippableItem>("Items/" + item));
        }

        //// Load Quest Progress from User into Game Quests
        //var questStates = QuestManager.instance.GetQuestStates();
        //var userQuestsContainer = User.GetActiveCharacter().GameState.QuestsContainer;
        //// dies here
        //if (userQuestsContainer != null)
        //{
        //    questStates.completedQuests = userQuestsContainer.completedQuests;
        //    questStates.activeQuests = userQuestsContainer.activeQuests;
        //    questStates.achievements = userQuestsContainer.achievements;
        //}
    }

    private IEnumerator LoadGameScene()
    {
        yield return StartCoroutine(SceneController.Instance.LoadFirstScene());
        hud.FindPlayerObject();
        hud.gameObject.SetActive(true);
    }

    private void onLogout()
    {
        logoutCanvas.gameObject.SetActive(true);
        UserService.Instance.SaveUser();
        AuthService.Instance.Logout();
        Application.Quit();
    }

    private void onExit()
    {
        Application.Quit();
    }
}
