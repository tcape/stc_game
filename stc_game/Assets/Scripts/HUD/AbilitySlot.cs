using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField] public Ability ability;
    [SerializeField] public Transform parent;
    public AbilityManager abilityManager;
    private SceneController sceneController;

    //private void Awake()
    //{
    //    sceneController = SceneController.Instance;
    //}

    //private void OnEnable()
    //{
    //    sceneController.AfterSceneLoad += GetAbilityManager;
    //}

    //private void OnDisable()
    //{
    //    sceneController.AfterSceneLoad -= GetAbilityManager;
    //}

    //private void GetAbilityManager()
    //{
    //    if (!abilityManager)
    //        abilityManager = FindObjectOfType<Hero>().abilityManager;
    //}

    private void RefreshButton()
    {
        var button = GetComponentInChildren<AbilityButton>();
        button.GetComponent<Image>().sprite = ability.sprite;
    }

    private void OnGUI()
    {
        RefreshButton();
    }
}
