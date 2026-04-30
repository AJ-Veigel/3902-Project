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
            game.inAnimation = true;
            AnimationManager.PipeAnimation(game.currentMario, pipe);
            game.spawnMarioAt(pipe.marioSpawnPos);
            game.toggleMap(level + bonus);
            game.inAnimation = false;
        }

        public static void GoToNextLevel(Game1 game, int currentLevel)
        {
            game.levelNumber = 4;
            game.gameTimer = 400f;
            if(currentLevel + 1 >= game.maps.Count)
            {
                currentLevel = -2;
            }
            game.spawnMarioAt(game.maps[currentLevel + 2].getSpawn());
            game.toggleMap(currentLevel + 2);
        }
    }
}