using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GameStrings
{
    public class GameStrings
    {
        public GameStrings Instance
        {
            get
            {
                if (this == null)
                {
                    return new GameStrings();
                }
                else
                {
                    return this;
                }
            }
            private set
            {
                Instance = this;
            }
        }

        string TownScene = "Town";
        string DungeonScene = "Dungeon";
        string LoginScene = "Login";
        string PersistentScene = "PersistentScene";

    }
}
