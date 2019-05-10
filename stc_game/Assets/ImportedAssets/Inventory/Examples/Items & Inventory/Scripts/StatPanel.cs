﻿using UnityEngine;
using UnityEngine.UI;

namespace Kryz.CharacterStats.Examples
{
    public class StatPanel : MonoBehaviour
    {
        [SerializeField] StatDisplay[] statDisplays;

        [SerializeField] Stats stats;

        public GameObject strengthValue;
        public GameObject dexterityValue;
        public GameObject intellectValue;
        public GameObject defenseValue;
        public GameObject attackValue;
        public GameObject critValue;
        public GameObject critMultiValue;
        public GameObject spellCritValue;
        public GameObject spellCritMultiValue;
        public GameObject dodgeValue;
        public GameObject movementValue;

        private void OnEnable()
        {
            UpdateStatValues();
        }

        private void Update()
        {
            UpdateStatValues();
        }

        private void OnValidate()
        {
            statDisplays = GetComponentsInChildren<StatDisplay>();
        }

        public void SetStats(params CharacterStat[] charStats)
        {
            //setup stats? I guess?
            //currently leaving this empty to avoid breaking external classes before I clean them up
        }

        public void UpdateStatValues()
        {
            stats = GetComponentInParent<InventoryPlayerTracker>().playerStats;

            strengthValue.GetComponentInChildren<Text>().text = stats.strength.currentValue.ToString();
            dexterityValue.GetComponentInChildren<Text>().text = stats.dexterity.currentValue.ToString();
            intellectValue.GetComponentInChildren<Text>().text = stats.intellect.currentValue.ToString();
            defenseValue.GetComponentInChildren<Text>().text = stats.strength.defense.currentValue.ToString();
            attackValue.GetComponentInChildren<Text>().text = stats.strength.attack.currentValue.ToString();
            critValue.GetComponentInChildren<Text>().text = (stats.dexterity.meleeCritRate.currentValue * 100).ToString() + "%";
            critMultiValue.GetComponentInChildren<Text>().text = stats.strength.meleeCritPower.currentValue.ToString() + "x";
            spellCritValue.GetComponentInChildren<Text>().text = (stats.intellect.abilityCritRate.currentValue * 100).ToString() + "%";
            spellCritMultiValue.GetComponentInChildren<Text>().text = stats.intellect.abilityCritPower.currentValue.ToString() + "x";
            dodgeValue.GetComponentInChildren<Text>().text = stats.dexterity.dodgeRate.currentValue.ToString();
            movementValue.GetComponentInChildren<Text>().text = stats.dexterity.movementSpeed.currentValue.ToString();
        }
    }
}