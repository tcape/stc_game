using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TownScene : MonoBehaviour
{
    private SceneController sceneController;
    private GameObject hero;
    
    private int num = 0;

    private void Awake()
    {
        sceneController = SceneController.Instance;

        if (sceneController && PersistentScene.Instance)
        {

            hero = Instantiate(Resources.Load(PersistentScene.Instance.GameCharacter.PrefabResource) as GameObject);
            hero.transform.parent = GameObject.FindGameObjectWithTag("HeroAndCamera").transform;
            hero.GetComponent<Hero>().LoadAbilities();
            hero.GetComponent<Hero>().LoadCharacterStats();
            Debug.Log("successish");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneController.Instance.FadeAndLoadScene(GameStrings.Scenes.DungeonScene);
        }
    }
}