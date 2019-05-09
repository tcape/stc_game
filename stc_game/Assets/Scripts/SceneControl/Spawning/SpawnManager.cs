using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public SpawnPositions spawnPositions;
    private SceneController sceneController;
    private Transform startPosition;
    [HideInInspector] public bool reviveInTown;

    private void Awake()
    {
        reviveInTown = PersistentScene.Instance.reviveController.reviveInTown;
        sceneController = FindObjectOfType<SceneController>();
        spawnPositions = FindObjectOfType<SpawnPositions>();

        if (!sceneController)
        {
            startPosition = spawnPositions.spawnPoints.First().transform;
            SetTransform(startPosition);
        }
        else if(reviveInTown)
        {
            startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals(GameStrings.Positions.InTownPosition)).SingleOrDefault().transform;
            SetTransform(startPosition);
            PersistentScene.Instance.reviveController.reviveInTown = false;
        }
        else
        {
            SetStartPosition(sceneController.previousSceneName, sceneController.currentSceneName);
            SetTransform(startPosition);
        }
    }
   
    private void SetStartPosition(string previous, string current)
    {
        var activeScene = SceneManager.GetActiveScene().name;
       
        // Dungeon to Town
        if (previous.Equals(GameStrings.Scenes.DungeonScene) && current.Equals(GameStrings.Scenes.TownScene))
        {
            startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals(GameStrings.Positions.CaveEntrancePosition)).SingleOrDefault().transform;

        }
        // Start Game
        else if (previous.Equals(GameStrings.Scenes.TownScene) && current.Equals(GameStrings.Scenes.TownScene))
        {
            startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals(GameStrings.Positions.CaveEntrancePosition)).SingleOrDefault().transform;
        }
        // Town to Dungeon
        else if (previous.Equals(GameStrings.Scenes.TownScene) && current.Equals(GameStrings.Scenes.DungeonScene))
        {
            startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals(GameStrings.Positions.DungeonStartPosition)).SingleOrDefault().transform;
        }
        else
            Debug.Log("Spawn position not set");
    }

    public void SetTransform(Transform setTransform)
    {
        transform.position = setTransform.position;
        transform.rotation = setTransform.rotation;
    }

    public Transform GetSpawnPoint(string spawnPositionName)
    {
        var spawnPoint = spawnPositions.spawnPoints.Where(p => p.name.Equals(spawnPositionName)).SingleOrDefault().transform;
        return spawnPoint;
    }
}
