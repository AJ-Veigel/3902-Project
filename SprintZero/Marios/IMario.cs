using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero.blocks;

namespace SprintZero.Marios
{
    public interface IMario
    {

        Vector2 location { get; set; }
        Rectangle MarioCollider { get; set; }
        bool Direction { get; set; }
        bool Jumping { get; set; }
        float jumpStartHeight { get; set; }
        bool isOnGround { get; set; }
        bool Moving { get; set; }
        bool Sprinting { get; set; }
        bool Swimming { get; set; }
        bool Crouching { get; set; }
        bool throwing { get; set; }
        bool Falling { get; set; }
        bool SlidingFlag { get; set; }
        bool Invincible { get; set; }
        bool AutoWalking { get; set; }
        bool WinState { get; set; }
        bool IsStarPower { get; set; }
        bool inPipe { get; set; }
        float pipeHeight { get; set; }
        IPipe pipeStorage { get; set; }
        float yVelocity { get; set; }
        float xVelocity { get; set; }
        float currentPlatformY { get; set; }

        void Bounce();
        void Move();
        void StopMove();
        void Jump();
        void Crouch();
        void Fireball();
        void Damage();
        void GrabFlagPole();
        void EndFlagPole();
        void BecomeInvincible();
        void SetLocation(Vector2 pos);
        Vector2 GetLocation();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void LandOnBlock(float blockTopY);

    }
}