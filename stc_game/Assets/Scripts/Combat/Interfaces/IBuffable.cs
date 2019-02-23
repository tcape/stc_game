using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.CharacterBehavior.Combat
{
    public interface IBuffable
    {
        void BuffMaxHP(double amount);
        void BuffMaxHP(float percentage);

        void BuffMaxAP(double amount);
        void BuffMaxAP(float percentage);

        void BuffCurrentHP(double amount);
        void BuffCurrentHP(float percentage);

        void BuffCurrentAP(double amount);
        void BuffCurrentAP(float percentage);

        void BuffStrength(double amount);
        void BuffStrength(float percentage);

        void BuffAttack(double amount);
        void BuffAttack(float percentage);

        void BuffAbilityAttack(double amount);
        void BuffAbilityAttack(float percentage);

        void BuffMeleeCritRate(double amount);
        void BuffMeleeCritRate(float percentage);

        void BuffMeleeCritPower(double amount);
        void BuffMeleeCritPower(float percentage);

        void BuffAbilityCritRate(double amount);
        void BuffAbilityCritRate(float percentage);

        void BuffAbilityCritPower(double amount);
        void BuffAbilityCritPower(float percentage);

        void BuffDefense(double amount);
        void BuffDefense(float percentage);

        void BuffDodgeRate(double amount);
        void BuffDodgeRate(float percentage);

        void BuffMovementSpeed(double amount);
        void BuffMovementSpeed(float percentage);
    }
}
