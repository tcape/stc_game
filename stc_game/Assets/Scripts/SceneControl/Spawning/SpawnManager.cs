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
    private string previousScene;
    private string currentScene;
    [HideInInspector] public bool reviveInTown;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
        spawnPositions = FindObjectOfType<SpawnPositions>();
        if (sceneController != null)
        {
            SetPreviousScene();
            SetCurrentScene();
        }
        SetStartPosition();
        transform.position = startPosition.position;
        transform.rotation = startPosition.rotation;
    }
    
    public void SetPreviousScene()
    {
        previousScene = sceneController.previousSceneName;
    }

    public void SetCurrentScene()
    {
        currentScene = sceneController.currentSceneName;
    }


    private void SetStartPosition()
    {
        var activeScene = SceneManager.GetActiveScene().name;
        // so hero will load in the level when not starting from persistent scene
        if (previousScene == null)
        {
            if (activeScene.Equals(GameStrings.Scenes.DungeonScene))
                startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals(GameStrings.Positions.DungeonStartPosition)).SingleOrDefault().transform;
            else if (activeScene.Equals(GameStrings.Scenes.TownScene))
                startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals(GameStrings.Positions.CaveEntrancePosition)).SingleOrDefault().transform;
            else
                startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals(GameStrings.Positions.CaveEntrancePosition)).SingleOrDefault().transform;
            return;
        }
        // for when we implement revive
        if (reviveInTown)
            startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals(GameStrings.Positions.InTownPosition)).SingleOrDefault().transform;
        // Dungeon to Town
        else if (previousScene.Equals(GameStrings.Scenes.DungeonScene) && currentScene.Equals(GameStrings.Scenes.TownScene))
        {
            startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals(GameStrings.Positions.CaveEntrancePosition)).SingleOrDefault().transform;
        }
        // Start Game
        else if (previousScene.Equals(GameStrings.Scenes.TownScene) && currentScene.Equals(GameStrings.Scenes.TownScene))
        {
            startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals(GameStrings.Positions.GameStartPosition)).SingleOrDefault().transform;
        }
        // Town to Dungeon
        else if (previousScene.Equals(GameStrings.Scenes.TownScene) && currentScene.Equals(GameStrings.Scenes.DungeonScene))
        {
            startPosition = spawnPositions.spawnPoints.Where(p => p.name.Equals(GameStrings.Positions.DungeonStartPosition)).SingleOrDefault().transform;
        }
        else
            Debug.Log("Starting Position not loaded correctly");
    }
}
