using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.CharacterBehavior.Combat
{
    public interface IAbility
    {
        bool CanUse(AbilityManager manager);
        void TriggerAnimator(AbilityManager manager);
    }
}
