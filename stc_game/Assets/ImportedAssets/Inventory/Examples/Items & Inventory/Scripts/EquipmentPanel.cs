using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kryz.CharacterStats.Examples
{
	public class EquipmentPanel : MonoBehaviour
	{
		[SerializeField] Transform equipmentSlotsParent;
		[SerializeField] public EquipmentSlot[] equipmentSlots;
        public List<EquippableItem> equipment;

		public event Action<Item> OnItemRightClickedEvent;

        private void Awake()
        {
            equipment = new List<EquippableItem>();
        }

        private void Start()
		{
			for (int i = 0; i < equipmentSlots.Length; i++)
			{
				equipmentSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
			}
		}
        
		private void OnValidate()
		{
			equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
		}

		public bool AddItem(EquippableItem item, out EquippableItem previousItem)
		{
			for (int i = 0; i < equipmentSlots.Length; i++)
			{
				if (equipmentSlots[i].EquipmentType == item.EquipmentType)
				{
					previousItem = (EquippableItem)equipmentSlots[i].Item;
					equipmentSlots[i].Item = item;
                    AddToEquipmentList(item);
					return true;
				}
			}
			previousItem = null;
			return false;
		}

		public bool RemoveItem(EquippableItem item)
		{
			for (int i = 0; i < equipmentSlots.Length; i++)
			{
				if (equipmentSlots[i].Item == item)
				{
					equipmentSlots[i].Item = null;
                    RemoveFromEquipmentList(item);
					return true;
				}
			}
			return false;
		}

        private void AddToEquipmentList(EquippableItem item)
        {
            if (!equipment.Contains(item))
            {
                equipment.Add(item);
            }
        }

        private void RemoveFromEquipmentList(EquippableItem item)
        {
            if (equipment.Contains(item))
            {
                equipment.Remove(item);
            }
        }
	}
}
