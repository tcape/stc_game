using UnityEngine;
using System.Collections.Generic;

namespace Kryz.CharacterStats.Examples
{
	public class PlatformGenerator : MonoBehaviour
	{
		public Platform PlatformPrefab;
		public GameObject Player;
		public float DiffXMax = 7;
		public float DiffYMax = 2;
		public float DiffZMin = 3;
		public float DiffZMax = 7;

		private int currentPlatformIndex = 0;
		private List<Platform> platforms = new List<Platform>();
		private List<Platform> platformPool = new List<Platform>();

		void Start()
		{
			for (int i = 0; i < 10; i++)
			{
				AddToPool(InstantiatePlatform());
			}

			Reset();
		}

		public void Reset()
		{
			for (int i = platforms.Count - 1; i >= 0; i--)
			{
				platforms[i].Reset();
				AddToPool(platforms[i]);
				platforms.RemoveAt(i);
			}

			currentPlatformIndex = 0;
			NewPlatform(currentPlatformIndex);
			currentPlatformIndex++;
		}

		private void NewPlatform(int index)
		{
			Vector3 nextPos;

			if (index == 0)
			{
				nextPos = Vector3.zero;
			}
			else
			{
				Platform prevPlatform = platforms[index - 1];
				Transform prevTransform = prevPlatform.transform;

				float nextX = Random.Range(-DiffXMax - prevTransform.localScale.x, DiffXMax + prevTransform.localScale.x) + prevTransform.localPosition.x;
				float nextY = Random.Range(-DiffYMax, DiffYMax) + prevTransform.localPosition.y;
				float nextZ = Random.Range(DiffZMin, DiffZMax) + prevTransform.localPosition.z + prevTransform.localScale.z / 2 + PlatformPrefab.transform.localScale.z / 2;
				nextPos = new Vector3(nextX, nextY, nextZ);
			}

			Platform platform = GetFromPool();
			platform.transform.localPosition = nextPos;
			platform.gameObject.SetActive(true);
			platforms.Add(platform);
		}

		private void OnPlatformCompleted(Platform platform)
		{
			NewPlatform(currentPlatformIndex);
			currentPlatformIndex++;
		}

		private Platform InstantiatePlatform()
		{
			Platform platform = (Platform)Instantiate(PlatformPrefab, Vector3.zero, Quaternion.identity);
			platform.transform.SetParent(transform, false);
			platform.PlatformCompleted += OnPlatformCompleted;
			platform.Player = Player;
			return platform;
		}

		private Platform GetFromPool()
		{
			if (platformPool.Count > 0) {
				Platform platform = platformPool[platformPool.Count - 1];
				platformPool.RemoveAt(platformPool.Count - 1);
				return platform;
			} else {
				return InstantiatePlatform();
			}
		}

		private void AddToPool(Platform platform)
		{
			platformPool.Add(platform);
			platform.gameObject.SetActive(false);
		}
	}
}
