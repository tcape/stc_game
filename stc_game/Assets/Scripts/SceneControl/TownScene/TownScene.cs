using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Devdog.QuestSystemPro.UI;
using Devdog.QuestSystemPro;

public class TownScene : MonoBehaviour
{
    private SceneController sceneController;
    private GameObject hero;
    
    private int num = 0;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
        
       
        if (sceneController && PersistentScene.Instance)
        {
             // if first time being loaded
            if (sceneController.previousSceneName.Equals(GameStrings.Scenes.TownScene))
            {
              
                hero = Instantiate(Resources.Load(PersistentScene.Instance.GameCharacter.PrefabResource) as GameObject);
                hero.transform.parent = GameObject.FindGameObjectWithTag("HeroAndCamera").transform;
                hero.GetComponent<Hero>().LoadAbilities();
                hero.GetComponent<Hero>().LoadCharacterStats();
            }
            else
            {
                hero = Instantiate(Resources.Load(PersistentScene.Instance.GameCharacter.PrefabResource) as GameObject);
                hero.transform.parent = GameObject.FindGameObjectWithTag("HeroAndCamera").transform;
                //hero.GetComponent<Hero>().characterStats.saver.Load();
                //hero.GetComponent<Hero>().abilityManager.saver.Load();
            }
            
        }
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneController.Instance.FadeAndLoadScene(GameStrings.Scenes.DungeonScene);
        }
    }


}