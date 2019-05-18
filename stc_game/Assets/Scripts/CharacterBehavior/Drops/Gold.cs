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
                // Call OnTriggerUsed here for gold quest instead of triggering with range
                GetComponent<SetQuestProgressOnTriggerObjectGold>().OnTriggerUsed(other.GetComponent<Player>());
                Destroy(gameObject);
            }
        }

    }
}
