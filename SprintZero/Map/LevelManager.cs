using System;
using Microsoft.Xna.Framework;
using SprintZero.blocks;

namespace SprintZero.Map
{
    public static class LevelManager
    {
        public static void SwapLevel(Game1 game, IPipe pipe)
        {
            int level = pipe.levelNum;
            int bonus = pipe.bonus;
            Console.WriteLine(level + "-" + bonus);
            level--;
            game.spawnMarioAt(pipe.marioSpawnPos);
            game.toggleMap(level + bonus);
        }

        public static void RetrunFromBonusLevel(Game1 game, IPipe pipe)
        {
            
        }

        public static void GoToNextLevel(Game1 game, int currentLevel)
        {
            game.spawnMarioAt(game.maps[currentLevel].getSpawn());
            game.toggleMap(currentLevel + 2);
        }
    }
}