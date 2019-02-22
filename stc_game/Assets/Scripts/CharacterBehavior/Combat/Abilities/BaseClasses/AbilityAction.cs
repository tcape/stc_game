using Assets.Scripts.CharacterBehavior.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CharacterBehavior.BaseClasses
{
    public abstract class AbilityAction : ScriptableObject
    {
        public double amount;
        public float percentage;
        public double effectTotal;
        [HideInInspector] public GameObject target;

        public enum ActionType { Instant, Periodic };
        public enum ActionPersistance { Permanent, Temporary };

        public ActionType type;
        public ActionPersistance persistance;
        /*[HideInInspector]*/ public float lastTick;
        public float interval;

        public abstract void Act(AbilityManager manager);

        public abstract void UpdateEffectTotal();

        public abstract void RemoveEffect(AbilityManager manager);

        public abstract void ResetEffectTotal();
    }
}
