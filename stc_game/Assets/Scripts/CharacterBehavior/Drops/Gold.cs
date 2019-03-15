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
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CharacterStats>().GainGold(amount);
                Destroy(gameObject);
            }
        }

    }
}
