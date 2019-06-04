using UnityEngine;

namespace Kryz.CharacterStats.Examples
{
    public enum ItemClass { Warrior, Mage, Any };

	[CreateAssetMenu]
	public class Item : ScriptableObject
	{
        public int Id;
        public ItemClass itemClass;
		public string ItemName;
		public Sprite Icon;
	}
}
