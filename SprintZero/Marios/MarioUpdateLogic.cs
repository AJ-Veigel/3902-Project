using SprintZero.blocks;
using SprintZero.Marios;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Sprites;

namespace SprintZero.MarioUpdate
{
    public static class MarioUpdateLogic
    {
        public static void pipeLogic(IMario mario, float pipeHeight, IPipe pipeStorage, MarioSprite marioSprites, Game1 game)
        {
            if (mario.inPipe)
            {
                if (pipeHeight > 0)
                {
                    mario.pipeHeight -= 4;
                    if (pipeStorage is TubeTop)
                    {
                        mario.location = new Vector2(mario.location.X, mario.location.Y + 4);
                    }
                    else
                    {
                        mario.location = new Vector2(mario.location.X + 4, mario.location.Y);
                    }
                    marioSprites.SetLocation(mario.location);
                    mario.MarioCollider = marioSprites.UpdateCollider();
                    return;
                }
                else
                {
                    mario.SetLocation(pipeStorage.marioSpawnPos);
                    game.toggleMap(pipeStorage.levelNum + pipeStorage.bonus - 1);
                    mario.pipeStorage = null;
                    mario.pipeHeight = 64;
                    mario.inPipe = false;
                }
            }
        }

        public static void flagLogic(IMario mario, float currentPlatformY, MarioSprite marioSprites)
        {
            if (mario.SlidingFlag)
            {
                float slideSpeed = 2.5f;

                Vector2 nextPosition = new Vector2(mario.location.X, mario.location.Y + slideSpeed);
                if (nextPosition.Y >= currentPlatformY)
                {
                    nextPosition.Y = currentPlatformY;
                    mario.location = nextPosition;
                    marioSprites.SetLocation(mario.location);

                    mario.EndFlagPole();
                }
                else
                {
                    mario.location = nextPosition;
                    marioSprites.SetLocation(mario.location);
                }

                mario.MarioCollider = marioSprites.UpdateCollider();
                return;
            }
            
            if (mario.AutoWalking)
            {
                float castleX = 100f;

                mario.xVelocity = 2f;
                mario.location = new Vector2(mario.location.X + mario.xVelocity, mario.location.Y);

                marioSprites.SetAnimatedSprite("moveRight");
                marioSprites.SetLocation(mario.location);

                if (mario.location.X >= castleX)
                {
                    mario.AutoWalking = false;
                    mario.xVelocity = 0;
                    marioSprites.SetSprite("standRight");
                }

                mario.MarioCollider = marioSprites.UpdateCollider();
                return;
            }
        }
    }
}