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
    [SerializeField] public Intellect intellect;
    [SerializeField] public Dexterity dexterity;

    [Space]
    public bool dead;
    [SerializeField] public double nextLevelXP;
    [SerializeField] public double totalXP;
    [HideInInspector] public static readonly double firstLevelXP = 100;

    private void Awake()
    {
        SetupMainStats();
    }

    private void Start()
    {
    }

    private void SetupMainStats()
    {
        strength.stats = this;
        intellect.stats = this;
        dexterity.stats = this;
    }

}
