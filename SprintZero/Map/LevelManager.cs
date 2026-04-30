using System;
using Microsoft.Xna.Framework;
using SprintZero.blocks;

namespace SprintZero.Map
{
    public static class LevelManager
    {
        public static void SwapLevel(Game1 game, IPipe pipe)
        {
            int level = pipe.levelNum - 1;
            int bonus = pipe.bonus;
            game.inAnimation = true;
            if(pipe is TubeTop)
            {
                game.animationState = Game1.AnimationType.PipeDown;
            }
            else if(pipe is TubeLeft)
            {
                game.animationState = Game1.AnimationType.PipeLeft;
            }
            game.pipeChange = pipe;
        }

        public static void GoToNextLevel(Game1 game, int currentLevel)
        {
            game.levelNumber = 4;
            game.gameTimer = 400f;
            if(currentLevel + 2 >= game.maps.Count)
            {
                currentLevel = -2;
            }
            game.spawnMarioAt(game.maps[currentLevel + 2].getSpawn());
            game.toggleMap(currentLevel + 2);
        }
    }
}