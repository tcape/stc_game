using UnityEngine;

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
        public GameCharacter gameCharacter = PersistentScene.Instance.GameCharacter;
        public int StrengthBonus;
		public int DexterityBonus;
		public int IntelligenceBonus;
		[Space]
		public float StrengthPercentBonus;
		public float DexterityPercentBonus;
		public float IntelligencePercentBonus;
		[Space]
		public EquipmentType EquipmentType;

		public void Equip(Character c)
		{
			if (StrengthBonus != 0)
				gameCharacter.Stats.strength += StrengthBonus;
			if (DexterityBonus != 0)
                gameCharacter.Stats.dexterity += DexterityBonus;
            if (IntelligenceBonus != 0)
                gameCharacter.Stats.intellect += IntelligenceBonus;

            if (StrengthPercentBonus != 0)
                gameCharacter.Stats.strength += StrengthPercentBonus;
            if (DexterityPercentBonus != 0)
                gameCharacter.Stats.dexterity += DexterityPercentBonus;
            if (IntelligencePercentBonus != 0)
                gameCharacter.Stats.intellect += IntelligencePercentBonus;
        }

		public void Unequip(Character c)
		{
            gameCharacter.Stats.strength -= StrengthBonus;
            gameCharacter.Stats.dexterity -= DexterityBonus;
            gameCharacter.Stats.intellect -= IntelligenceBonus;
        }
    }
}