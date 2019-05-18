using UnityEngine;
using System.Collections;
using System;
using Devdog.General;
using Assets.Scripts.CharacterBehavior.Drops;

namespace Devdog.QuestSystemPro
{
    [AddComponentMenu(QuestSystemPro.AddComponentMenuPath + "Set Quest Progress/Set Quest Progress On Trigger Object")]
    [RequireComponent(typeof(Trigger))]
    public sealed class SetQuestProgressOnTriggerObjectGold : MonoBehaviour, ITriggerCallbacks
    {
        private enum Use
        {
            OnUse,
            OnUnUse
        }

        public Gold gold;

        public void Start()
        {
            gold = gameObject.GetComponent<Gold>();
        }

        public QuestProgressDecorator progress;


        [Header("Trigger configuration")]
        [SerializeField]
        private Use _use;


        public bool OnTriggerUsed(Player player)
        {
            if (_use == Use.OnUse)
            {
                progress.progress += (float)gold.amount;
                progress.Execute();
                Debug.Log("Gold given to quest onUse " + gold.amount.ToString());
            }

            return false;
        }

        public bool OnTriggerUnUsed(Player player)
        {
            if (_use == Use.OnUnUse)
            {
                progress.progress += (float)gold.amount;
                progress.Execute();
                Debug.Log("Gold given to quest onUnuse " + gold.amount.ToString());
            }

            return false;
        }
    }
}