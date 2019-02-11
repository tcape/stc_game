using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats: MonoBehaviour
{
    public CharacterBaseModel baseStats;
    public double MaxHP;
    public double MaxAP;
    public double CurrentHP;
    public double CurrentAP;
    public double Strength;
    public double Attack;
    public double AbilityAttack;
    public double MeleeCritRate;
    public double AbilityCritRate;
    public double Defense;
    public double DodgeRate;
    public double AttackSpeed;
    public double MovementSpeed;
    private bool Dead;

    private void Awake()
    {
        MaxHP = baseStats.HealthPoints;
        MaxAP = baseStats.AbilityPoints;
        CurrentHP = baseStats.HealthPoints;
        CurrentAP = baseStats.AbilityPoints;
        Strength = baseStats.Strength;
        Attack = baseStats.Attack;
        AbilityAttack = baseStats.AbilityAttack;
        MeleeCritRate = baseStats.CriticalRate;
        AbilityCritRate = baseStats.AbilityCriticalRate;
        Defense = baseStats.Defense;
        DodgeRate = baseStats.Dodge;
        AttackSpeed = baseStats.AttackSpeed;
        MovementSpeed = baseStats.MovementSpeed;
        Dead = false;
    }
    
}
