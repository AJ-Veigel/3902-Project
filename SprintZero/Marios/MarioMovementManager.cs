using Microsoft.Xna.Framework;
using SprintZero.Marios;
using SoundManager;

namespace MarioMovement
{
    public static class MarioMovementManager
    {
        public static void Move(IMario mario, MarioSprite sprites)
        {
            int DefaultMoveSpeed = 4;
            mario.Moving = true;
            mario.xVelocity = mario.Direction ? DefaultMoveSpeed : -DefaultMoveSpeed;
            if (mario.Jumping && !mario.isOnGround && !mario.throwing && !mario.Moving)
            {
                sprites.SetSprite(mario.Direction ? "jumpRight" : "jumpLeft");
            }
            else if (!mario.Jumping)
            {
                sprites.SetAnimatedSprite(mario.Direction ? "moveRight" : "moveLeft");
            }
            mario.location = new Vector2(mario.location.X + mario.xVelocity, mario.location.Y);
            sprites.SetLocation(mario.location);
        }

        public static void StopMove(IMario mario, MarioSprite sprites)
        {
            mario.Moving = false;
            mario.xVelocity = 0;
            if (!mario.Jumping && !mario.Falling && !mario.throwing && !mario.Crouching)
            {
                sprites.SetSprite(mario.Direction ? "standRight" : "standLeft");
            }
        }

        public static void Bounce(IMario mario, MarioSprite sprites)
        {
            mario.yVelocity = -6f;

            mario.Jumping = true;
            mario.Falling = false;
            mario.isOnGround = false;
            mario.jumpStartHeight = mario.location.Y;

            sprites.SetSprite(mario.Direction ? "jumpRight" : "jumpLeft");

            if (mario is SmallMario) Music.jumpSmallSound.Play();
            else Music.jumpBigSound.Play();
        }

        public static void Jump(IMario mario, MarioSprite sprites)
        {
            if (mario.isOnGround)
            {
                mario.yVelocity = -11;
                mario.Jumping = true;
                mario.Falling = false;
                mario.jumpStartHeight = mario.location.Y;
                mario.isOnGround = false;

                // Update sprite
                sprites.SetSprite(mario.Direction ? "jumpRight" : "jumpLeft");

                if (mario is SmallMario) Music.jumpSmallSound.Play();
                else Music.jumpBigSound.Play();
            }
        }

        public static void GrabFlagPole(IMario mario, MarioSprite sprites)
        {
            mario.SlidingFlag = true;

            mario.Jumping = false;
            mario.Falling = false;
            mario.isOnGround = false;

            mario.xVelocity = 0f;
            mario.yVelocity = 0f;

            sprites.SetAnimatedSprite(mario.Direction ? "flagpoleRight" : "flagpoleLeft");
        }

        public static void EndFlagPole(IMario mario, MarioSprite sprites)
        {
            mario.SlidingFlag = false;
            mario.AutoWalking = true;
            mario.yVelocity = 0f;
            mario.xVelocity = 0f;
            mario.isOnGround = true;
            mario.Falling = false;
            mario.Jumping = false;

            mario.currentPlatformY = mario.location.Y;


            mario.location = new Vector2(mario.location.X, mario.currentPlatformY);
            sprites.SetLocation(mario.location);
            sprites.SetAnimatedSprite("moveRight");
        }

        public static void LandOnBlock(IMario mario, MarioSprite sprites, float blockTopY)
        {
            mario.location = new Vector2(mario.location.X, blockTopY - mario.MarioCollider.Height);
            mario.isOnGround = true;
            mario.Jumping = false;
            mario.Falling = false;
            mario.jumpStartHeight = mario.location.Y;

            mario.yVelocity = 0;

            sprites.SetSprite(mario.Direction ? "standRight" : "standLeft");
        }
    }
}