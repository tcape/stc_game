using Assets.Scripts.MonoBehaviors;
using UnityEngine;
using UnityEngine.AI;

public class CharacterStats : MonoBehaviour
{
    public StatsPreset presetStats;
    public CharacterStatsSaver saver;
    [SerializeField] public Stats stats;

    private void Awake()
    {
        stats = new Stats();
        
        if (gameObject.CompareTag("Player"))
        {
            saver = GetComponent<CharacterStatsSaver>();
        }
        else
            LoadPresetStats();
    }

    private void Update()
    {
        GetComponent<NavMeshAgent>().speed = (float)stats.dexterity.movementSpeed.currentValue;
    }

    public void LoadPresetStats()
    {
        stats.level = presetStats.level;
        stats.gold = presetStats.gold;
        stats.XP = presetStats.XP;
        stats.nextLevelXP = stats.NextLevelXPAmount();
        stats.strength.maxHP.baseValue = presetStats.maxHP;
        stats.intellect.maxAP.baseValue = presetStats.maxAP;
        stats.currentHP = presetStats.maxHP;
        stats.currentAP = presetStats.maxAP;
        stats.strength.baseValue = presetStats.strength;
        stats.intellect.baseValue = presetStats.intellect;
        stats.dexterity.baseValue = presetStats.dexterity;
        stats.strength.attack.baseValue = presetStats.attack;
        stats.intellect.abilityAttack.baseValue = presetStats.abilityAttack;
        stats.dexterity.meleeCritRate.baseValue = presetStats.meleeCritRate;
        stats.strength.meleeCritPower.baseValue = presetStats.meleeCritPower;
        stats.intellect.abilityCritRate.baseValue = presetStats.abilityCritRate;
        stats.intellect.abilityCritPower.baseValue = presetStats.abilityCritPower;
        stats.strength.defense.baseValue = presetStats.defense;
        stats.dexterity.dodgeRate.baseValue = presetStats.dodgeRate;
        stats.dexterity.movementSpeed.baseValue = presetStats.movementSpeed;
        stats.dead = false;
    }
}
