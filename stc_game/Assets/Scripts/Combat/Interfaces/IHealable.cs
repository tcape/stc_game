using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.CharacterBehavior.Combat
{
    public interface IHealable
    {
        void Heal(double amount);
        void Heal(float percentage);    }
}
