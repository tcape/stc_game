using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ReviveController : MonoBehaviour
{
    public GameObject reviveAtEntrance;
    private Canvas reviveCanvas;
    public bool reviveInTown;

    private void Awake()
    {
        reviveCanvas = gameObject.GetComponent<Canvas>();
        reviveInTown = false;
    }

    private void Update()
    {
        if (PersistentScene.Instance.GameCharacter.Stats.dead)
            reviveCanvas.enabled = true;
        else
            reviveCanvas.enabled = false;

        if (SceneController.Instance.currentSceneName.Equals(GameStrings.Scenes.TownScene))
            reviveAtEntrance.SetActive(false);
        else
            reviveAtEntrance.SetActive(true);
    }

    public void ReviveInTown()
    {
        reviveInTown = true;
        // get dead hero
        var deadPlayer = GameObject.FindGameObjectWithTag("Player");
        var deadHero = deadPlayer.GetComponent<Hero>();

        PersistentScene.Instance.SaveGameCharacterStats(deadHero.characterStats.stats);

        // destroy dead hero
        if (SceneController.Instance.currentSceneName.Equals(GameStrings.Scenes.TownScene))
        {
            Destroy(deadPlayer);
        }

        // load and switch scenes to town scene (this will also save hero's stats to persistent GameCharacter)
        SceneController.Instance.FadeAndLoadScene(GameStrings.Scenes.TownScene);

        // get new hero
        var player = GameObject.FindGameObjectWithTag("Player");
        var hero = player.GetComponent<Hero>();

        // set values for hero's components
        PersistentScene.Instance.GameCharacter.Stats.dead = false;
        hero.characterStats.stats.dead = false;
        hero.animator.SetBool("Dead", false);
        hero.navMeshAgent.enabled = true;
        hero.rigidbody.isKinematic = false;
        hero.physicsCollider.enabled = true;
        hero.stateController.currentState = hero.stateController.startState;

        // give hero some HP
        hero.characterStats.stats.currentHP = hero.characterStats.stats.maxHP;

        // disable the revive canvas
        reviveCanvas.enabled = false;
    }

    public void ReviveAtBody()
    {
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
        hero.navMeshAgent.enabled = true;
        hero.rigidbody.isKinematic = false;
        hero.physicsCollider.enabled = true;
        hero.stateController.currentState = hero.stateController.startState;
        hero.abilityManager.ActivatePassiveAbilites();

        // disable the revive canvas
        reviveCanvas.enabled = false;

        // take away some gold? or remove some durability from equipment(not yet implemented)
        //hero.characterStats.stats.LoseGold(100);
    }

    public void ReviveAtEntrance()
    {
        PersistentScene.Instance.GameCharacter.Stats.dead = false;

        StartCoroutine(MoveHero());
    }

    private IEnumerator MoveHero()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var hero = player.GetComponent<Hero>();

        // set hero's dead stat to false
        hero.characterStats.stats.dead = false;

        PersistentScene.Instance.SaveGameCharacterStats(hero.characterStats.stats);

        yield return StartCoroutine(SceneController.Instance.FadeOut());

        yield return new WaitForSeconds(1f);

        // give hero some HP
        hero.characterStats.stats.BuffCurrentHP(hero.characterStats.stats.maxHP / 2);

        // move hero to entrance
        hero.SetHeroTransform(GameStrings.Positions.DungeonStartPosition);

        // set values for hero's components
        hero.navMeshAgent.enabled = true;
        hero.rigidbody.isKinematic = false;
        hero.physicsCollider.enabled = true;
        hero.stateController.currentState = hero.stateController.startState;
        hero.abilityManager.ActivatePassiveAbilites();

        // drop some gold maybe?

        // trigger the revive animation
        hero.animator.SetBool("Dead", false);

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(SceneController.Instance.FadeIn());
    }
}
