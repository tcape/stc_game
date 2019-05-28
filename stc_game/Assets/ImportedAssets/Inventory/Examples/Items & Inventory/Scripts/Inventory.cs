using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kryz.CharacterStats.Examples
{
    public class Inventory : MonoBehaviour
    {
        private HeroClass heroClass;
        [SerializeField] public List<Item> items;
        [SerializeField] Transform itemsParent;
        [SerializeField] ItemSlot[] itemSlots;

        public event Action<Item> OnItemRightClickedEvent;

        private void Start()
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
            }

            RefreshUI();
        }

        private void Awake()
        {
            heroClass = PersistentScene.Instance.GameCharacter.HeroClass;

            // will need to check if first time playing the game with character
            SetStartingItems();
            RefreshUI();
        }

        private void SetStartingItems()
        {
            switch (heroClass)
            {
                case HeroClass.Warrior:
                    items = WarriorStartingItems.startingItems;
                    break;
                case HeroClass.Mage:
                    items = MageStartingItems.startingItems;
                    break;
            }
        }

		private void OnValidate()
		{
			if (itemsParent != null)
				itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

			RefreshUI();
		}

		private void RefreshUI()
		{
			int i = 0;
			for (; i < items.Count && i < itemSlots.Length; i++)
			{
				itemSlots[i].Item = items[i];
			}

			for (; i < itemSlots.Length; i++)
			{
				itemSlots[i].Item = null;
			}
		}

		public bool AddItem(Item item)
		{
			if (IsFull())
				return false;

			items.Add(item);
			RefreshUI();
			return true;
		}

		public bool RemoveItem(Item item)
		{
			if (items.Remove(item))
			{
				RefreshUI();
				return true;
			}
			return false;
		}

		public bool IsFull()
		{
			return items.Count >= itemSlots.Length;
		}
	}
}
