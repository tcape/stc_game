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
        GetComponent<NavMeshAgent>().speed = (float)stats.movementSpeed;
    }

    public void LoadPresetStats()
    {
        stats.level = presetStats.level;
        stats.gold = presetStats.gold;
        stats.XP = presetStats.XP;
        stats.nextLevelXP = stats.NextLevelXPAmount();
        stats.maxHP = presetStats.maxHP;
        stats.maxAP = presetStats.maxAP;
        stats.currentHP = presetStats.maxHP;
        stats.currentAP = presetStats.maxAP;
        stats.strength = presetStats.strength;
        stats.intellect = presetStats.intellect;
        stats.dexterity = presetStats.dexterity;
        stats.attack = presetStats.attack;
        stats.abilityAttack = presetStats.abilityAttack;
        stats.meleeCritRate = presetStats.meleeCritRate;
        stats.meleeCritPower = presetStats.meleeCritPower;
        stats.abilityCritRate = presetStats.abilityCritRate;
        stats.abilityCritPower = presetStats.abilityCritPower;
        stats.defense = presetStats.defense;
        stats.dodgeRate = presetStats.dodgeRate;
        stats.movementSpeed = presetStats.movementSpeed;
        stats.dead = false;
    }
}
