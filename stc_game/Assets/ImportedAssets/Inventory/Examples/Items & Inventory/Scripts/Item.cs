using UnityEngine;
using System;

namespace Kryz.CharacterStats.Examples
{
    public enum ItemClass { Warrior, Mage, Any };

	[CreateAssetMenu]
	public class Item : ScriptableObject
	{
        public string Id;
        public ItemClass itemClass;
		public string ItemName;
		public Sprite Icon;
	}
}
