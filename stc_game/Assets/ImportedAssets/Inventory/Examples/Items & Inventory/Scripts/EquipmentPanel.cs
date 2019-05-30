using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kryz.CharacterStats.Examples
{
	public class EquipmentPanel : MonoBehaviour
	{
        public GameObject characterPanel;
		[SerializeField] Transform equipmentSlotsParent;
		[SerializeField] public EquipmentSlot[] equipmentSlots;
        public List<EquippableItem> equipment;
		public event Action<Item> OnItemRightClickedEvent;
        private Hero hero;


        private void Awake()
        {
            characterPanel = GameObject.Find("Character Panel");
            equipment = new List<EquippableItem>();
            SceneController.Instance.AfterSceneLoad += GetHero;
            characterPanel.SetActive(false);
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
                    if (previousItem)
                        RemoveFromEquipmentList(previousItem);
                    AddToEquipmentList(item);
                    hero.OnEquipmentChange(equipment);
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
                    hero.OnEquipmentChange(equipment);
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

        public void GetHero()
        {
            hero = GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>();
        }
	}
}
