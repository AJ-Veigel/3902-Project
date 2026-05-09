using SprintZero.blocks;
using SprintZero.Marios;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Sprites;
using MonoGame.Extended.Serialization.Json;

namespace SprintZero.MarioUpdate
{
    public static class MarioUpdateLogic
    {
        private const float GRAVITY = 0.2f;
        private const float JUMP_POWER = -11f;

        public static void marioUpdate(IMario mario, float pipeHeight, IPipe pipeStorage, MarioSprite marioSprites, Game1 game)
        {
            flagLogic(mario, marioSprites);
            pipeLogic(mario, pipeHeight, pipeStorage, marioSprites, game);
            movementLogic(mario, marioSprites);
        }

        private static void pipeLogic(IMario mario, float pipeHeight, IPipe pipeStorage, MarioSprite marioSprites, Game1 game)
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

        private static void flagLogic(IMario mario, MarioSprite marioSprites)
        {
            if (mario.SlidingFlag)
            {
                float slideSpeed = 2.5f;

                Vector2 nextPosition = new Vector2(mario.location.X, mario.location.Y + slideSpeed);
                if (nextPosition.Y >= mario.currentPlatformY)
                {
                    nextPosition.Y = mario.currentPlatformY;
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

        private static void movementLogic(IMario mario, MarioSprite marioSprites)
        {
            if (mario.Jumping)
            {
                mario.yVelocity += GRAVITY;
                mario.location = new Vector2(mario.location.X, mario.location.Y + mario.yVelocity);

                if (mario.yVelocity > 0)
                {
                    mario.Falling = true;
                    mario.Jumping = false;
                }
            }

            if (mario.Falling)
            {
                if (mario.yVelocity <= -JUMP_POWER)
                    mario.yVelocity += GRAVITY;
                mario.location = new Vector2(mario.location.X, mario.location.Y + mario.yVelocity);

                if (mario.isOnGround)
                {
                    mario.yVelocity = 0;
                    mario.location = new Vector2(mario.location.X, mario.currentPlatformY);
                    mario.Jumping = false;
                    mario.Falling = false;

                    marioSprites.SetSprite(mario.Direction ? "standRight" : "standLeft");
                }
            }

            if ((mario.Jumping || mario.Falling) && !mario.isOnGround)
            {
                marioSprites.SetSprite(mario.Direction ? "jumpRight" : "jumpLeft");
            }
        }
    }
}