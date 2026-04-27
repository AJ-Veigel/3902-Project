using System;
using SprintZero.blocks;

namespace SprintZero.Map
{
    public static class LevelManager
    {
        public static void SwapLevel(Game1 game, IPipe pipe)
        {
            int level = pipe.levelNum;
            int bonus = pipe.bonus;
            level--;
            game.toggleMap(level + bonus);
            game.spawnMarioAt(pipe.marioSpawnPos);
        }

        public static void RetrunFromBonusLevel(Game1 game, IPipe pipe)
        {
            
        }

        public static void GoToNextLevel()
        {

        }
    }
}