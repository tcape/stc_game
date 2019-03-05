using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MonoBehaviors
{
    public class CharacterStatsSaver : Saver
    {

        public CharacterStats characterStats;

        

        protected override void Load()
        {
            CharacterStats stats = new CharacterStats();
            if (saveData.Load(key, ref stats))
            {
                characterStats = stats;
            }
        }

        protected override void Save()
        {
            characterStats = GetComponent<CharacterStats>();

            saveData.Save(key, characterStats);
        }

        protected override string SetKey()
        {
            return characterStats.name + characterStats.GetType().FullName + uniqueIdentifier;
        }
    }
}
