using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
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
}
