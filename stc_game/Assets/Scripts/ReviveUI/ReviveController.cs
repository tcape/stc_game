using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ReviveController : MonoBehaviour
{
    public GameObject reviveAtEntrance;
    public Canvas reviveCanvas;
    public bool reviveInTown;
    private SceneController sceneController;

    private void Awake()
    {
        reviveCanvas = gameObject.GetComponent<Canvas>();
        reviveCanvas.enabled = false;
        reviveInTown = false;
        sceneController = SceneController.Instance;
    }

    private void Update()
    {
        if (sceneController)
        {
            if (sceneController.currentSceneName.Equals(GameStrings.Scenes.TownScene))
                reviveAtEntrance.SetActive(false);
            else
                reviveAtEntrance.SetActive(true);
        }
    }

    public void ReviveInTown()
    {
        // disable the revive canvas
        reviveCanvas.enabled = false;

        reviveInTown = true;
        // get dead hero
        var deadPlayer = GameObject.FindGameObjectWithTag("Player");
        var deadHero = deadPlayer.GetComponent<Hero>();

        PersistentScene.Instance.SaveGameCharacterStats(deadHero.characterStats.stats);

        // destroy dead hero
        if (SceneController.Instance.currentSceneName.Equals(GameStrings.Scenes.TownScene))
            Destroy(deadPlayer);

        // Fade and switch scenes to town scene (this will also save hero's stats to persistent GameCharacter)
        SceneController.Instance.FadeAndLoadScene(GameStrings.Scenes.TownScene);

        // get new hero
        var player = GameObject.FindGameObjectWithTag("Player");
        var hero = player.GetComponent<Hero>();

        // set values for hero's components
        PersistentScene.Instance.GameCharacter.Stats.dead = false;

        hero.SetReviveComponents();

        // give hero some HP
        hero.characterStats.stats.currentHP = hero.characterStats.stats.maxHP;
        
    }

    public void ReviveAtBody()
    {
        // disable the revive canvas
        reviveCanvas.enabled = false;

        // get dead hero
        var player = GameObject.FindGameObjectWithTag("Player");
        var hero = player.GetComponent<Hero>();

        // give hero some HP
        hero.characterStats.stats.BuffCurrentHP(hero.characterStats.stats.maxHP / 2);

        // set hero's dead stat to false
        hero.characterStats.stats.dead = false;
        PersistentScene.Instance.GameCharacter.Stats.dead = false;

        // trigger the revive animation
        hero.animator.SetBool("Dead", false);

        // set values for hero's components
        hero.SetReviveComponents();

      
        // take away some gold? or remove some durability from equipment(not yet implemented)
        //hero.characterStats.stats.LoseGold(100);
    }

    public void ReviveAtEntrance()
    {
        // disable the revive canvas
        reviveCanvas.enabled = false;

        StartCoroutine(MoveHeroToEntrance());
    }

    private IEnumerator MoveHeroToEntrance()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var hero = player.GetComponent<Hero>();

        // save stats to persistent GameCharacter
        PersistentScene.Instance.SaveGameCharacterStats(hero.characterStats.stats);

        // Fade out
        yield return StartCoroutine(SceneController.Instance.FadeOut());

        yield return new WaitForSeconds(1f);

        // set hero's dead stat to false
        hero.characterStats.stats.dead = false;

        PersistentScene.Instance.GameCharacter.Stats.dead = false;

        // give hero some HP
        hero.characterStats.stats.BuffCurrentHP(hero.characterStats.stats.maxHP / 2);

        // move hero to entrance
        hero.SetHeroTransform(GameStrings.Positions.DungeonStartPosition);

        // set values for hero's components
        hero.SetReviveComponents();

        // drop some gold maybe?

        // trigger the revive animation
        hero.animator.SetBool("Dead", false);

        yield return new WaitForSeconds(1f);
        
        // Fade in
        yield return StartCoroutine(SceneController.Instance.FadeIn());
    }
}
