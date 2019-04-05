using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Transform startPosition;
    public SpawnPositions spawnPositions;
    [HideInInspector] public bool reviveInTown;
    private string previousScene;
    private string currentScene;

    private void Awake()
    {
        spawnPositions = FindObjectOfType<SpawnPositions>();
    }
    

    public void SetScenes(string previousSceneName, string sceneName)
    {
        previousScene = previousSceneName;
        currentScene = sceneName;
    }

    private void Start()
    {
        SetStartPosition();

        transform.position = startPosition.position;
        transform.rotation = startPosition.rotation;
    }

    private void SetStartPosition()
    {
        if (reviveInTown)
            startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals("ReviveInTownPosition")).SingleOrDefault().transform;

        else if (previousScene.Equals(GameStrings.Scenes.DungeonScene) && currentScene.Equals(GameStrings.Scenes.TownScene))
        {
            startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals("CaveEntrancePosition")).SingleOrDefault().transform;
        }
        // start game
        else if (previousScene.Equals(GameStrings.Scenes.TownScene) && currentScene.Equals(GameStrings.Scenes.TownScene))
        {
            startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals("ReviveInTownPosition")).SingleOrDefault().transform;
        }

        else if (previousScene.Equals(GameStrings.Scenes.TownScene) && currentScene.Equals(GameStrings.Scenes.DungeonScene))
        {
            startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals("DungeonStartPosition")).SingleOrDefault().transform;
        }
    }
}
