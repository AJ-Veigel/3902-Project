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
            game.currentMario.inPipe = true;
            game.currentMario.pipeStorage = pipe;
        }

        public static void GoToNextLevel(Game1 game, int currentLevel)
        {
            game.levelNumber = 4;
            game.gameTimer = 400f;
            game.spawnMarioAt(game.maps[currentLevel + 2].getSpawn());
            game.toggleMap(currentLevel + 2);
        }
    }
}