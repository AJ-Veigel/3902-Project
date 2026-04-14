using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        bool Sprinting { get; set; }
        bool Swimming { get; set; }
        bool Crouching { get; set; }
        bool Falling { get; set; }
        bool SlidingFlag { get; set; }
        bool Invincible { get; set; }

        void Bounce();
        void Move();
        void StopMove();
        void Jump();
        void Crouch();
        void Fireball();
        void Damage();
        void GrabFlagPole();
        void EndFlagPole();
        float yVelocity { get; set; }
        float xVelocity { get; set; }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void LandOnBlock(float blockTopY);

    }
}