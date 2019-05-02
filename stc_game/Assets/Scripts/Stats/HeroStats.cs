using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    [Space]
    [SerializeField] public double level;
    [SerializeField] public double XP;
    [SerializeField] public double gold;

    [SerializeField] public double currentHP;
    [SerializeField] public double currentAP;
    [Space]
    [SerializeField] public Strength strength;
    [SerializeField] public MainStat intellect;
    [SerializeField] public MainStat dexterity;

    public bool dead;

    [SerializeField] public double nextLevelXP;
    [SerializeField] public double totalXP;
    [HideInInspector] public static readonly double firstLevelXP = 100;

}
