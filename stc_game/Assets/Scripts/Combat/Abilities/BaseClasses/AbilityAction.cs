using Assets.Scripts.CharacterBehavior.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CharacterBehavior.BaseClasses
{
    public enum ActionType { Instant, Periodic };
    public enum ActionPersistance { Permanent, Temporary };

    public abstract class AbilityAction : ScriptableObject
    {
        public ActionType type;
        public ActionPersistance persistance;
        public double amount;
        public float percentage;
        public float interval;
        [HideInInspector] public double effectTotal;
        [HideInInspector] public GameObject target;
        [HideInInspector] public float lastTick;
       

        public abstract void Act(AbilityManager manager);

        public abstract void UpdateEffectTotal();

        public abstract void RemoveEffect(AbilityManager manager);

        public abstract void ResetEffectTotal();

        
    }
}
