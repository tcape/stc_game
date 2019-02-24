using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.CharacterBehavior.Combat
{
    public interface IDamageable
    {
        void TakeDamage(double amount);

        void TakeMeleeDamage(CharacterStats other);
        double CalculateMeleeDamage(CharacterStats other);

        // TODO: Make methods for each damage dealing ability
        void TakeAbilityDamage(CharacterStats other);

        void TakeAbilityDamage(CharacterStats other, double multiplier);

        double CalculateAblilityDamage(CharacterStats other);

        double CalculateAbilityDamage(CharacterStats other, double multiplier);
        
       
    }
}
