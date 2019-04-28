using UnityEngine;

namespace Kryz.CharacterStats.Examples
{
	[RequireComponent(typeof(Rigidbody))]
	public class Movement : MonoBehaviour
	{
		public Rigidbody Rigidbody;
		public float MovementSpeed = 6;
		public float JumpForce = 6;
		public float JumpControlMultiplier = 0.05f;

		private bool isGrounded = false;

		void FixedUpdate()
		{
			Move();
			Jump();
		}

		void OnValidate()
		{
			if (Rigidbody == null)
				Rigidbody = GetComponent<Rigidbody>();
		}

		void OnCollisionStay()
		{
			isGrounded = true;
		}

		void OnCollisionExit()
		{
			isGrounded = false;
		}

		private void Move()
		{
			Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			Vector3 transformedVelocity = transform.TransformDirection(velocity);
			transformedVelocity *= MovementSpeed;

			Vector3 velocityChange = transformedVelocity - Rigidbody.velocity;
			velocityChange.y = 0;

			if (isGrounded) {
				Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			} else {
				Rigidbody.AddForce(velocityChange * JumpControlMultiplier, ForceMode.VelocityChange);
			}
		}

		private void Jump()
		{
			if (!isGrounded) return;

			if (Input.GetButton("Jump"))
			{
				Rigidbody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
			}
		}
	}
}
