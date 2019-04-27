using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType { HP, AP };

public class PotionButton : MonoBehaviour
{
    public Transform parent;
    public PotionType type;
    public float cooldown;
    public PotionCooldown potionCooldown;

    private void Start()
    {
        potionCooldown = parent.GetComponentInChildren<PotionCooldown>();
    }

    public void UsePotion()
    {
        var stats = FindObjectOfType<Hero>().characterStats.stats;

        if (potionCooldown.image.fillAmount == 0f)
        {
            potionCooldown.StartCooldown();

            if (type == PotionType.HP)
                stats.BuffCurrentHP(stats.maxHP);

            if (type == PotionType.AP)
                stats.BuffCurrentAP(stats.maxAP);
        }
    }
}
