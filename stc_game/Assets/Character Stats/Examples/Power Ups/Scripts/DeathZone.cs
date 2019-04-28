using UnityEngine;

namespace Kryz.CharacterStats.Examples
{
	public class DeathZone : MonoBehaviour
	{
		[SerializeField] Transform playerTransform;
		[SerializeField] PlatformGenerator platformGenerator;

		private float deathZoneY;
		private Vector3 startingPlayerPos;

		void Start()
		{
			deathZoneY = transform.position.y;
			startingPlayerPos = playerTransform.position;
		}

		void Update()
		{
			Vector3 newPos = playerTransform.position;
			newPos.y = deathZoneY;
			transform.position = newPos;
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.transform == playerTransform)
			{
				playerTransform.position = startingPlayerPos;
				platformGenerator.Reset();
			}
		}
	}
}
