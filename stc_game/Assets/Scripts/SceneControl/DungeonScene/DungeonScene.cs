using Devdog.QuestSystemPro;
using Devdog.QuestSystemPro.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScene : MonoBehaviour
{
    private int num = 0;
    private SceneController sceneController;
    private GameObject hero;


    private void Awake()
    {
        sceneController = SceneController.Instance;
        if (sceneController && PersistentScene.Instance)
        {
            if (sceneController.previousSceneName.Equals(GameStrings.Scenes.TownScene))
            {
                hero = Instantiate(Resources.Load(PersistentScene.Instance.GameCharacter.PrefabResource) as GameObject);
                hero.transform.parent = GameObject.FindGameObjectWithTag("HeroAndCamera").transform;
                hero.GetComponent<Hero>().LoadCharacterStats();
                hero.GetComponent<Hero>().abilityManager.saver.Load();
                // Turn on hero lights
                foreach (var light in hero.GetComponentsInChildren<Light>())
                {
                    light.enabled = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneController.Instance.FadeAndLoadScene(GameStrings.Scenes.TownScene);
        }
    }
}
