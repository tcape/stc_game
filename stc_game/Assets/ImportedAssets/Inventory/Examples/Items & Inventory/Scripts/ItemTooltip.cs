using UnityEngine;
using UnityEngine.UI;
using System.Text;

namespace Kryz.CharacterStats.Examples
{
	public class ItemTooltip : MonoBehaviour
	{
		public static ItemTooltip Instance;

		[SerializeField] Text nameText;
		[SerializeField] Text slotTypeText;
		[SerializeField] Text statsText;

		private StringBuilder sb = new StringBuilder();

		private void Awake()
		{
			if (Instance == null) {
				Instance = this;
			} else {
				Destroy(this);
			}
			gameObject.SetActive(false);
		}

		public void ShowTooltip(Item itemToShow)
		{
			if (!(itemToShow is EquippableItem)) {
				return;
			}

			EquippableItem item = (EquippableItem)itemToShow;

			gameObject.SetActive(true);

			nameText.text = item.ItemName;
			slotTypeText.text = item.EquipmentType.ToString();

			sb.Length = 0;

			AddStatText(item.strengthBonus, " Strength");
			AddStatText(item.dexterityBonus, " Dexterity");
			AddStatText(item.intellectBonus, " Intellect");
            AddStatText(item.attackBonus, " Attack");
            AddStatText(item.defenseBonus, " Defense");
            AddStatText(item.critChanceBonus, " Crit Chance");
            AddStatText(item.critPowerBonus, " Crit Power");
            AddStatText(item.abilityCritChanceBonus, " Ability Crit Chance");
            AddStatText(item.abilityCritPowerBonus, " Ability Crit Power");
            AddStatText(item.dodgeBonus * 100, " Dodge Chance");
            AddStatText(item.movementBonus, " Movement");

            AddStatText(item.strengthPercentBonus * 100, "% Strength");
			AddStatText(item.dexterityPercentBonus * 100, "% Dexterity");
			AddStatText(item.intellectPercentBonus * 100, "% Intellect");
            AddStatText(item.attackPercentBonus * 100, "% Attack");
            AddStatText(item.defensePercentBonus * 100, "% Defense");
            AddStatText(item.critChancePercentBonus * 100, "% Crit Chance");
            AddStatText(item.critPowerPercentBonus * 100, "% Crit Power");
            AddStatText(item.abilityCritChancePercentBonus * 100, "% Ability Crit Chance");
            AddStatText(item.abilityCritPowerPercentBonus * 100, "% Ability Crit Power");
            AddStatText(item.dodgePercentBonus * 100, "% Dodge Chance");
            AddStatText(item.movementPercentBonus * 100, "% Movement");

            statsText.text = sb.ToString();
		}

		public void HideTooltip()
		{
			gameObject.SetActive(false);
		}

		private void AddStatText(float statBonus, string statName)
		{
			if (statBonus != 0)
			{
				if (sb.Length > 0)
					sb.AppendLine();

				if (statBonus > 0)
					sb.Append("+");

				sb.Append(statBonus);
				sb.Append(statName);
			}
		}
	}
}
