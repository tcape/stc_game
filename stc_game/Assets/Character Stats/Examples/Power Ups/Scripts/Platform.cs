using System;
using UnityEngine;

namespace Kryz.CharacterStats.Examples
{
	public class Platform : MonoBehaviour
	{
		[SerializeField] Powerup[] powerups;
		public bool IsCompleted;
		public GameObject Player;

		public event Action<Platform> PlatformCompleted;

		void Start()
		{
			Reset();
		}

		void OnValidate()
		{
			powerups = GetComponentsInChildren<Powerup>();
		}

		void OnCollisionEnter(Collision collision)
		{
			if (!IsCompleted && collision.collider.gameObject == Player)
			{
				IsCompleted = true;

				if (PlatformCompleted != null)
					PlatformCompleted.Invoke(this);
			}
		}

		public void Reset()
		{
			IsCompleted = false;

			for (int i = 0; i < powerups.Length; i++)
			{
				powerups[i].gameObject.SetActive(true);
			}
		}
	}
}
