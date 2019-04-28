using UnityEngine;
using System.Collections;

namespace Kryz.CharacterStats.Examples
{
	public class Powerup : MonoBehaviour
	{
		public float MovementSpeedBonus;
		public float JumpForceBonus;
		public float Duration;

		protected void Apply(Player player)
		{
			player.MovementSpeed.AddModifier(new StatModifier(MovementSpeedBonus, StatModType.Flat, this));
			player.JumpForce.AddModifier(new StatModifier(JumpForceBonus, StatModType.Flat, this));

			player.StartCoroutine(Timer(Duration, player));
		}

		protected void OnTriggerEnter(Collider other)
		{
			Player player = other.GetComponent<Player>();
			if (player != null)
			{
				Apply(player);
				gameObject.SetActive(false);
			}
		}

		private IEnumerator Timer(float duration, Player player)
		{
			yield return new WaitForSeconds(duration);
			player.MovementSpeed.RemoveAllModifiersFromSource(this);
			player.JumpForce.RemoveAllModifiersFromSource(this);
		}
	}
}
