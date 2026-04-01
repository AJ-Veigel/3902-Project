using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero.Marios
{
    public interface IMario
    {

        Vector2 location { get; set; }
        Rectangle MarioCollider { get; set; }
        Boolean Direction { get; set; }
        Boolean Jumping { get; set; }
        float jumpStartHeight { get; set; }
        Boolean isOnGround { get; set; }
        Boolean Sprinting { get; set; }
        Boolean Swimming { get; set; }
        Boolean Crouching { get; set; }
        Boolean Falling { get; set; }
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