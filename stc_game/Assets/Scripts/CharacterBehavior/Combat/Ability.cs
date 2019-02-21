using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="PluggableAI/Actions/Ability")]
public class Ability : CharacterAction
{
    private float lastCalled = 0f;
    public string trigger;
    public float cooldown;

    public override void Act(StateController controller)
    {
        PerformAbility(controller);
    }

    private void PerformAbility(StateController controller)
    {
        // Hotkey pushed?
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // First time used
            if (lastCalled == 0f)
            {
                lastCalled = Time.time;
                // Perform Ability
                Debug.Log("Ability Performed");
                controller.animator.SetTrigger(trigger);
            }
            else
            {
                // Check for cooldown
                var now = Time.time;
                var timeSinceLastCalled = now - lastCalled;

                if (timeSinceLastCalled > cooldown)
                {
                    lastCalled = Time.time;
                    // Perform Ability
                    Debug.Log("Ability Performed");
                    controller.animator.SetTrigger(trigger);
                }
                else
                {
                    // Still on cooldown
                    Debug.Log("Ability on Cooldown");
                }
            }
        }
    }

    private void Awake()
    {
        lastCalled = 0f;
    }

    private void OnEnable()
    {
        lastCalled = 0f;
    }

}
