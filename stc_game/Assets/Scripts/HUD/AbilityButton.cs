using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [SerializeField] public Transform parent;
    [SerializeField] private Sprite image;
    private Ability ability;


    private void OnGUI()
    {
        ability = parent.GetComponent<AbilitySlot>().ability;
    }

    public void ExecuteAbility()
    {
        var manager = parent.GetComponentInParent<ActionBarController>().abilityManager;

        manager.ActivateAbility(ability);
    }

}
