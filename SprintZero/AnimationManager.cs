using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero.blocks;
using SprintZero.Marios;

namespace SprintZero
{
    public static class AnimationManager
    {
        public static void PipeAnimation()
        {

        }

        public static void DrawAnimation(SpriteBatch spritebatch, Game1 game, IPipe pipe)
        {
            if (pipe != null)
            {
                switch (game.animationState)
                {
                    case Game1.AnimationType.PipeDown:
                        {
                            int marioBottom = game.currentMario.MarioCollider.Bottom;
                            for (int i = game.currentMario.MarioCollider.Top; i < marioBottom; i += 4)
                            {
                                Vector2 marioPos = new Vector2(game.currentMario.location.X, i);
                                game.currentMario.SetLocation(marioPos);
                                game.currentMario.Draw(spritebatch);
                            }
                            break;
                        }
                    case Game1.AnimationType.PipeLeft:
                        {
                            int marioRight = game.currentMario.MarioCollider.Right;
                            for (int i = game.currentMario.MarioCollider.Left; i < marioRight; i += 4)
                            {
                                Vector2 marioPos = new Vector2(i, game.currentMario.location.Y);
                                game.currentMario.SetLocation(marioPos);
                                game.currentMario.Draw(spritebatch);
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                game.spawnMarioAt(pipe.marioSpawnPos);
                game.toggleMap(pipe.levelNum + pipe.bonus - 1);
            }
            game.inAnimation = false;
        }
    }
}