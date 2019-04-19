using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveController : MonoBehaviour
{
    private Canvas reviveCanvas;

    private void Awake()
    {
        reviveCanvas = gameObject.GetComponent<Canvas>();
    }

    private void Update()
    {
        if (PersistentScene.Instance.GameCharacter.Stats.dead)
            reviveCanvas.enabled = true;
        else
            reviveCanvas.enabled = false;
    }

    public void ReviveInTown()
    {
        // load and switch scenes to town scene (this will also save hero's stats to persistent GameCharacter)
        
        // make sure hero's dead stat is no longer true

        // set spawn point to town position

        // give hero some HP

        // disable the revive canvas
    }

    public void ReviveAtBody()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var hero = player.GetComponent<Hero>();

        // give hero some HP
        hero.characterStats.stats.BuffCurrentHP(hero.characterStats.stats.maxHP / 2);
        

        // set hero's dead stat to false
        hero.characterStats.stats.dead = false;
        PersistentScene.Instance.GameCharacter.Stats.dead = false;

        // trigger the revive animation
        hero.animator.SetBool("Dead", false);
        
        // enable 
        hero.navMeshAgent.enabled = true;
        hero.rigidbody.isKinematic = false;
        hero.physicsCollider.enabled = true;
        hero.stateController.currentState = hero.stateController.startState;

        // disable the revive canvas
        reviveCanvas.enabled = false;
        // take away some gold? or remove some durability from equipment(not yet implemented)

        //hero.characterStats.stats.LoseGold(100);
    }

    public void ReviveAtEntrance()
    {
        // give hero some HP

        // set hero's dead stat to false

        // save hero's stats to persistent GameCharacter

        // intantiate new hero at entrance point

        // destroy dead hero's GameObject

        // drop some gold maybe?
    }

}
