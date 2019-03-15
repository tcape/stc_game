using Assets.Scripts.CharacterBehavior.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CharacterBehavior.Combat
{
    public class AbilityManager : MonoBehaviour
    {
        public List<Ability> myAbilities;
        private List<Ability> activeAbilites;
        [HideInInspector] public CharacterStats stats;
        [HideInInspector] public Animator animator;
        [HideInInspector] public StateController controller;

        private void Awake()
        {
            stats = GetComponent<CharacterStats>();
            animator = GetComponent<Animator>();
            activeAbilites = new List<Ability>();
            controller = GetComponent<StateController>();
            foreach (var ability in myAbilities)
            {
                if (ability.targetType == TargetType.Self)
                    ability.target = gameObject;
                if (ability.abilityType == AbilityType.AlwaysActive)
                    activeAbilites.Add(ability);
            }
        }

        private void Update()
        {
            foreach (var ability in myAbilities)
            {
                if (Input.GetKeyDown(ability.hotkey))
                {
                    if (ability.targetType.Equals(TargetType.Self))
                    {
                        ability.target = gameObject;
                    }
                   else
                        ability.target = controller.target;

                    if(ability.CanUse(this))
                    {
                        activeAbilites.Add(ability);
                        stats.UseAbilityPoints(ability.cost);
                        ability.TriggerAnimator(this);
                        ability.startTime = Time.time;
                        foreach (var action in ability.actions)
                            action.target = ability.target;
                        foreach (var action in ability.actions.Where(t => t.type.Equals(ActionType.Instant)))
                        {
                            action.Act(this);
                        }
                    }
                    
                }
            }

            foreach (var ability in activeAbilites)
            {
                if (ability.target.GetComponent<CharacterStats>().dead)
                {
                    foreach(var action in ability.actions)
                    {
                        action.RemoveEffect(this);
                    }

                    activeAbilites.Remove(ability);
                }

                if (ability.startTime + ability.duration <= Time.time && ability.abilityType == AbilityType.Activate)
                {
                    foreach (var action in ability.actions)
                    {
                        if (action.persistance.Equals(ActionPersistance.Temporary))
                            action.RemoveEffect(this);

                        action.ResetEffectTotal();
                    }

                    activeAbilites.Remove(ability);
                }
               
                else
                {
                    foreach (var action in ability.actions)
                    {
                        if (action.type.Equals(ActionType.Periodic))
                        {
                            if (Time.time > action.lastTick + action.interval)
                            {
                                // do the stuff
                                action.Act(this);
                                // update last tick
                                action.lastTick = Time.time;
                            }
                        }
                    }
                }
            }
        }
    }
}
