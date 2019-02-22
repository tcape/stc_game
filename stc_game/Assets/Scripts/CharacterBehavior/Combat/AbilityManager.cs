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
        public List<Ability> allAbiliies;
        private List<Ability> activeAbilites;
        [HideInInspector] public CharacterStats stats;
        [HideInInspector] public Animator animator;

        private void Awake()
        {
            stats = GetComponent<CharacterStats>();
            animator = GetComponent<Animator>();
            activeAbilites = new List<Ability>();
        }

        private void Update()
        {
            foreach (var ability in allAbiliies)
            {
                if (Input.GetKeyDown(ability.hotkey) && ability.CanUse())
                {
                    activeAbilites.Add(ability);
                    ability.TriggerAnimator(this);
                    ability.startTime = Time.time;
                    foreach (var action in ability.actions.Where(t => t.type.Equals(AbilityAction.ActionType.Instant)))
                    {
                        action.Act(this);
                    }
                }
            }

            foreach (var ability in activeAbilites)
            {
                if (ability.startTime + ability.duration < Time.time)
                {
                    foreach (var action in ability.actions)
                    {
                        if (action.persistance.Equals(AbilityAction.ActionPersistance.Temporary))
                            action.RemoveEffect(this);

                        action.ResetEffectTotal();
                    }

                    activeAbilites.Remove(ability);
                }
                else
                {
                    foreach (var action in ability.actions)
                    {
                        if (action.type.Equals(AbilityAction.ActionType.Periodic))
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
