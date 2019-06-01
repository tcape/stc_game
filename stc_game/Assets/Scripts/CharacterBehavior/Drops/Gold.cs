using Devdog.General;
using Devdog.QuestSystemPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CharacterBehavior.Drops

{
    public class Gold : MonoBehaviour
    {
        public double amount;
        private Quest goldQuest;

        private void Awake()
        {
            goldQuest = Resources.Load<Quest>("Quest/Quest-Collect300Gold");
        }

        public void SetAmount(double value)
        {
            amount = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && other.isTrigger)
            {
                other.GetComponent<CharacterStats>().stats.GainGold(amount);
                Debug.Log("Gold given to hero " + amount.ToString());
               
                if (QuestManager.instance.HasActiveQuest(goldQuest))
                {
                    GetComponent<SetQuestProgressOnTriggerObjectGold>().OnTriggerUsed(other.GetComponent<Player>());
                }

                Destroy(gameObject);
            }
        }

    }
}
