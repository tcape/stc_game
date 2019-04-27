using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarController : MonoBehaviour
{
    [SerializeField]
    public AbilitySlot[] abilitySlots;
    [SerializeField]
    public AbilityManager abilityManager;
    [SerializeField]
    public List<Ability> myAbilities = new List<Ability>();
    private SceneController sceneController;

    private void Awake()
    {
        sceneController = SceneController.Instance;
        var slots = transform.GetComponentsInChildren<AbilitySlot>();
        abilitySlots = slots;
    }

    private void Start()
    {
        PersistentScene.Instance.actionBar = this;
        SetupActionBar();
    }

    private void OnEnable()
    {
        sceneController.AfterSceneLoad += GetAbilityManager;
    }

    private void OnDisable()
    {
        sceneController.AfterSceneLoad -= GetAbilityManager;
    }

    private void GetAbilityManager()
    {
        if (!abilityManager)
            abilityManager = FindObjectOfType<Hero>().abilityManager;
    }

    private void OnValidate()
    {
        if (abilitySlots.Length == 0)
        {
            var slots = transform.GetComponentsInChildren<AbilitySlot>();

            abilitySlots = slots;
        }

        LoadAbilities();
    }

    public void LoadAbilities()
    {
        if (myAbilities.Count > 0)
        {
            for (var i = 0; i < myAbilities.Count; i++)
            {
                abilitySlots[i].ability = myAbilities[i];
            }
        }
    }

    private void SetupActionBar()
    {
        myAbilities = PersistentScene.Instance.GameCharacter.GetAbilities();
        enabled = true;
        LoadAbilities();
    }
}
