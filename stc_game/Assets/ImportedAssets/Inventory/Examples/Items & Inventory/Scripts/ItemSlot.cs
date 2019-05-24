using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Kryz.CharacterStats.Examples
{
	public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] Image image;

		public event Action<Item> OnRightClickEvent;

		private Item _item;
		public Item Item {
			get { return _item; }
			set {
				_item = value;

				if (_item == null) {
					image.enabled = false;
				} else {
					image.sprite = _item.Icon;
					image.enabled = true;
				}
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
			{
				if (Item != null && OnRightClickEvent != null)
					OnRightClickEvent(Item);
			}
		}

		protected virtual void OnValidate()
		{
			if (image == null)
				image = GetComponent<Image>();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			ItemTooltip.Instance.ShowTooltip(Item);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			ItemTooltip.Instance.HideTooltip();
		}
	}
}
