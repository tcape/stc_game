using UnityEngine;
using UnityEngine.UI;

namespace Kryz.CharacterStats.Examples
{
    public class StatPanel : MonoBehaviour
    {
        [SerializeField] StatDisplay[] statDisplays;

        [SerializeField] Stats stats;

        public GameObject characterLevel;
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

        public void UpdateStatValues()
        {
            //var tracker = GetComponentInParent<InventoryPlayerTracker>();
            //tracker.FindPlayerObject();
            //stats = tracker.playerStats;

            stats = PersistentScene.Instance.GameCharacter.Stats;
            characterLevel.GetComponentInChildren<Text>().text = stats.level.ToString();
            strengthValue.GetComponentInChildren<Text>().text = stats.strength.currentValue.ToString("0.##");
            dexterityValue.GetComponentInChildren<Text>().text = stats.dexterity.currentValue.ToString("0.##");
            intellectValue.GetComponentInChildren<Text>().text = stats.intellect.currentValue.ToString("0.##");
            defenseValue.GetComponentInChildren<Text>().text = stats.strength.defense.currentValue.ToString("0.##");
            attackValue.GetComponentInChildren<Text>().text = stats.strength.attack.currentValue.ToString("0.##");
            critValue.GetComponentInChildren<Text>().text = (stats.dexterity.meleeCritRate.currentValue * 100).ToString("0.##") + "%";
            critMultiValue.GetComponentInChildren<Text>().text = stats.strength.meleeCritPower.currentValue.ToString("0.##") + "x";
            spellCritValue.GetComponentInChildren<Text>().text = (stats.intellect.abilityCritRate.currentValue * 100).ToString("0.##") + "%";
            spellCritMultiValue.GetComponentInChildren<Text>().text = stats.intellect.abilityCritPower.currentValue.ToString("0.##") + "x";
            dodgeValue.GetComponentInChildren<Text>().text = (stats.dexterity.dodgeRate.currentValue * 100).ToString("0.#") + "%";
            movementValue.GetComponentInChildren<Text>().text = stats.dexterity.movementSpeed.currentValue.ToString("0.#");
        }
    }
}