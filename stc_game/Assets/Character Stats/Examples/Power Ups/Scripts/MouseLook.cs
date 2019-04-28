using UnityEngine;

namespace Kryz.CharacterStats.Examples
{
	public class MouseLook : MonoBehaviour
	{
		public Transform RotateHorizontally;
		public Transform RotateVertically;
		public float MouseSensitivity = 120;
		public float MaxLook = 80;
		public float MinLook = -60;

		private void OnValidate()
		{
			if (RotateHorizontally == null)
				RotateHorizontally = transform;

			if (RotateVertically == null && transform.childCount > 0)
				RotateVertically = transform.GetChild(0);
		}

		private void Update()
		{
			CameraLook();
		}

		private void CameraLook()
		{
			float rotationX = Input.GetAxis("Mouse X") * Time.deltaTime;
			float rotationY = -Input.GetAxis("Mouse Y") * Time.deltaTime;

			if (rotationX != 0)
			{
				RotateHorizontally.transform.Rotate(0, rotationX * MouseSensitivity, 0);
			}

			if (rotationY != 0)
			{
				Vector3 angles = RotateVertically.transform.localEulerAngles;
				angles.x += rotationY * MouseSensitivity;

				if (angles.x > MaxLook && angles.x < MinLook + 360) {
					float diff1 = Mathf.DeltaAngle(angles.x, MinLook);
					float diff2 = Mathf.DeltaAngle(angles.x, MaxLook);
					angles.x = Mathf.Abs(diff1) < Mathf.Abs(diff2) ? MinLook : MaxLook;
				}

				RotateVertically.transform.localEulerAngles = angles;
			}
		}
	}
}
