using Assets.Scripts.CharacterBehavior.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarController : MonoBehaviour
{
    [SerializeField]  public AbilitySlot[] abilitySlots;
    [SerializeField] public AbilityManager abilityManager;
    [SerializeField] public List<Ability> myAbilities = new List<Ability>();
    private SceneController sceneController;
    private Hero hero;

    private void Awake()
    {
        sceneController = SceneController.Instance;
        var slots = transform.GetComponentsInChildren<AbilitySlot>();

        abilitySlots = slots;
    }

    private void Start()
    {

    }

    private void OnValidate()
    {

        if (abilitySlots.Length == 0)
        {
            var slots = transform.GetComponentsInChildren<AbilitySlot>();

            abilitySlots = slots;
        }

        Refresh();
            
    }

    public void Refresh()
    {
        int i = 0;
        if (myAbilities.Count > 0)
        {
            for (; i < myAbilities.Count; i++)
            {
                abilitySlots[i].ability = myAbilities[i];
            }
        }
      
    }

    private void LoadAbilities()
    {
        var slots = transform.GetComponentsInChildren<AbilitySlot>();
        for (var i = 0; i < abilitySlots.Length; i++)
        {
            slots[i].ability = myAbilities[i];
        }
    }
}
