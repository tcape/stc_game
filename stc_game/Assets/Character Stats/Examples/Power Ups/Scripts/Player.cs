using UnityEngine;

namespace Kryz.CharacterStats.Examples
{
	public class Player : MonoBehaviour
	{
		public Movement Movement;
		public CharacterStat MovementSpeed;
		public CharacterStat JumpForce;

		void Start()
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}

		void OnValidate()
		{
			if (Movement == null)
				Movement = GetComponent<Movement>();
		}

		void Update()
		{
			Movement.MovementSpeed = MovementSpeed.Value;
			Movement.JumpForce = JumpForce.Value;
		}
	}
}
