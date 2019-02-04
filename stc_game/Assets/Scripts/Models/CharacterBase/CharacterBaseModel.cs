using System;

[Serializable]
public class CharacterBaseModel
{
    public double Level { get; private set; }
    public double HealthPoints { get; private set; }
    public double AbilityPoints { get; set; }
    public double Strength { get; private set; }
    public double Attack { get; private set; }
    public double AbilityAttack { get; private set; }
    public double CriticalRate { get; private set; }
    public double AbilityCriticalRate { get; private set; }
    public double Defense { get; private set; }
    public double Dodge { get; private set; }
    public double AttackSpeed { get; private set; }
    public double MovementSpeed { get; private set; }
}
