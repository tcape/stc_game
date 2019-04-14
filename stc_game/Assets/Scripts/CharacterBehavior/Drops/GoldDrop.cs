using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CharacterBehavior.Drops
{
    public class GoldDrop : MonoBehaviour
    {
        public GameObject gold;
        private GameObject goldInstance;
        public Vector3 offset;
        private Stats stats;
        private bool dropped;

        private void Start()
        {
            goldInstance = Instantiate(gold);
            stats = GetComponent<CharacterStats>().stats;
            goldInstance.GetComponent<Gold>().SetAmount(stats.gold);
            goldInstance.SetActive(false);
            dropped = false;
        }

        public void DropGold()
        {
            if (!dropped)
            {
                goldInstance.SetActive(true);
                goldInstance.transform.position = transform.position + offset;
                goldInstance.transform.rotation = Quaternion.identity;
                dropped = true;
            }
        }
    }
}
