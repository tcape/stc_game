using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStrings
{
    public static class Scenes
    {
        public const string TownScene = "Town";
        public const string DungeonScene = "Dungeon";
        public const string LoginScene = "Login";
        public const string PersistentScene = "PersistentScene";
    }

    public static class Positions
    {
        public const string GameStartPosition = "GameStartPosition";
        public const string InTownPosition = "InTownPosition";
        public const string CaveEntrancePosition = "CaveEntrancePosition";
        public const string DungeonStartPosition = "DungeonStartPosition";
    }

    public static class LocalStorage
    {
        public const string AuthToken = "AuthToken";
    }
}
