using UnityEngine;
using UnityEngine.UI;

namespace Kryz.CharacterStats.Examples
{
	public class PlayerBarUI : MonoBehaviour
	{
		[SerializeField] Player player;
		[SerializeField] Image speedImage;
		[SerializeField] Image jumpImage;
		[SerializeField] float speedminValue = 2;
		[SerializeField] float jumpminValue = 2;
		[SerializeField] float speedMaxValue = 14;
		[SerializeField] float jumpMaxValue = 14;

		private float speedMaxSize;
		private float jumpMaxSize;

		void OnValidate()
		{
			if (player == null)
				player = FindObjectOfType<Player>();
		}

		void Awake()
		{
			speedMaxSize = speedImage.rectTransform.sizeDelta.y;
			jumpMaxSize = jumpImage.rectTransform.sizeDelta.y;
		}

		void Update()
		{
			Vector3 speedSize = speedImage.rectTransform.sizeDelta;
			speedSize.y = (player.MovementSpeed.Value - speedminValue) / speedMaxValue * speedMaxSize;
			speedImage.rectTransform.sizeDelta = speedSize;

			Vector3 jumpSize = jumpImage.rectTransform.sizeDelta;
			jumpSize.y = (player.JumpForce.Value - jumpminValue) / jumpMaxValue * jumpMaxSize;
			jumpImage.rectTransform.sizeDelta = jumpSize;
		}
	}
}
