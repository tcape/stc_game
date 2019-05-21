using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.MonoBehaviors;

namespace Kryz.CharacterStats.Examples
{
	public enum EquipmentType
	{
		Helmet,
		Chest,
		Gloves,
		Boots,
		Weapon1,
		Weapon2,
		Accessory1,
		Accessory2,
	}
    
	[CreateAssetMenu]
	public class EquippableItem : Item
	{
        public GameObject characterPanel;

		public int strengthBonus;
		public int dexterityBonus;
		public int intellectBonus;
        [Space]
        public int attackBonus;
        public int defenseBonus;
        public float critChanceBonus;
        public float critPowerBonus;
        public float abilityCritChanceBonus;
        public float abilityCritPowerBonus;
        public float dodgeBonus;
        public float movementBonus;
		[Space]
		public float strengthPercentBonus;
		public float dexterityPercentBonus;
		public float intellectPercentBonus;
        [Space]
        public float attackPercentBonus;
        public float defensePercentBonus;
        public float critChancePercentBonus;
        public float critPowerPercentBonus;
        public float abilityCritChancePercentBonus;
        public float abilityCritPowerPercentBonus;
        public float dodgePercentBonus;
        public float movementPercentBonus;
        [Space]
		public EquipmentType EquipmentType;

		public void Equip(InventoryManager c)
		{
            var stats = c.GetComponent<InventoryPlayerTracker>().playerStats;

            //strength
            if (strengthBonus != 0)
                stats.strength.AddStatModifier(new StatModifier(strengthBonus, ModType.Flat, this));
            if (strengthPercentBonus != 0)
                stats.strength.AddStatModifier(new StatModifier(strengthPercentBonus, ModType.PercentAdd, this));
            //dexterity
            if (dexterityBonus != 0)
                stats.dexterity.AddStatModifier(new StatModifier(dexterityBonus, ModType.Flat, this));
            if (dexterityPercentBonus != 0)
                stats.dexterity.AddStatModifier(new StatModifier(dexterityPercentBonus, ModType.PercentAdd, this));
            //intellect
            if (intellectBonus != 0)
                stats.intellect.AddStatModifier(new StatModifier(intellectBonus, ModType.Flat, this));
            if (intellectPercentBonus != 0)
                stats.intellect.AddStatModifier(new StatModifier(intellectPercentBonus, ModType.PercentAdd, this));
            //attack
            if (attackBonus != 0)
                stats.strength.attack.AddModifier(new StatModifier(attackBonus, ModType.Flat, this));
            if (attackPercentBonus != 0)
                stats.strength.attack.AddModifier(new StatModifier(attackPercentBonus, ModType.PercentAdd, this));
            //defense
            if (defenseBonus != 0)
                stats.strength.defense.AddModifier(new StatModifier(defenseBonus, ModType.Flat, this));
            if (defensePercentBonus != 0)
                stats.strength.defense.AddModifier(new StatModifier(defensePercentBonus, ModType.PercentAdd, this));
            //crit chance
            if (critChanceBonus != 0)
                stats.dexterity.meleeCritRate.AddModifier(new StatModifier(critChanceBonus, ModType.Flat, this));
            if (critChancePercentBonus != 0)
                stats.dexterity.meleeCritRate.AddModifier(new StatModifier(critChancePercentBonus, ModType.PercentAdd, this));
            //crit power
            if (critPowerBonus != 0)
                stats.strength.meleeCritPower.AddModifier(new StatModifier(critPowerBonus, ModType.Flat, this));
            if (critPowerPercentBonus != 0)
                stats.strength.meleeCritPower.AddModifier(new StatModifier(critPowerPercentBonus, ModType.PercentAdd, this));
            //ability crit chance
            if (abilityCritChanceBonus != 0)
                stats.intellect.abilityCritRate.AddModifier(new StatModifier(abilityCritChanceBonus, ModType.Flat, this));
            if (abilityCritChancePercentBonus != 0)
                stats.intellect.abilityCritRate.AddModifier(new StatModifier(abilityCritChancePercentBonus, ModType.PercentAdd, this));
            //ability crit power
            if (abilityCritPowerBonus != 0)
                stats.intellect.abilityCritPower.AddModifier(new StatModifier(abilityCritPowerBonus, ModType.Flat, this));
            if (abilityCritPowerPercentBonus != 0)
                stats.intellect.abilityCritPower.AddModifier(new StatModifier(abilityCritPowerPercentBonus, ModType.PercentAdd, this));
            //dodge chance
            if (dodgeBonus != 0)
                stats.dexterity.dodgeRate.AddModifier(new StatModifier(dodgeBonus, ModType.Flat, this));
            if (dodgePercentBonus != 0)
                stats.dexterity.dodgeRate.AddModifier(new StatModifier(dodgePercentBonus, ModType.PercentAdd, this));
            //movement speed
            if (movementBonus != 0)
                stats.dexterity.movementSpeed.AddModifier(new StatModifier(movementBonus, ModType.Flat, this));
            if (movementPercentBonus != 0)
                stats.dexterity.movementSpeed.AddModifier(new StatModifier(movementPercentBonus, ModType.PercentAdd, this));

            c.GetComponentInChildren<StatPanel>().UpdateStatValues();
        }

		public void Unequip(InventoryManager c)
		{
            var stats = c.GetComponent<InventoryPlayerTracker>().playerStats;

            stats.strength.RemoveAllModifiersFromSource(this);
            stats.dexterity.RemoveAllModifiersFromSource(this);
            stats.intellect.RemoveAllModifiersFromSource(this);
            stats.strength.attack.RemoveAllModifiersFromSource(this);
            stats.strength.defense.RemoveAllModifiersFromSource(this);
            stats.strength.meleeCritPower.RemoveAllModifiersFromSource(this);
            stats.dexterity.meleeCritRate.RemoveAllModifiersFromSource(this);
            stats.dexterity.dodgeRate.RemoveAllModifiersFromSource(this);
            stats.dexterity.movementSpeed.RemoveAllModifiersFromSource(this);
            stats.intellect.abilityAttack.RemoveAllModifiersFromSource(this);
            stats.intellect.abilityCritRate.RemoveAllModifiersFromSource(this);
            stats.intellect.abilityCritPower.RemoveAllModifiersFromSource(this);

            c.GetComponentInChildren<StatPanel>().UpdateStatValues();
            stats.strength.UpdateSubStatModifiers();
            stats.dexterity.UpdateSubStatModifiers();
            stats.intellect.UpdateSubStatModifiers();
        }
    }
}