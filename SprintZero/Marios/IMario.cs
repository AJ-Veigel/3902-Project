using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero;

namespace SpriteZero.Marios
{
    public interface IMario : ICollidable
    {

        Rectangle MarioCollider { get; set; }
        Boolean Direction { get; set; }
        Boolean Jumping { get; set; }
        float jumpStartHeight {get;set;}
        Boolean isOnGround {get;set;}
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
        float yVelocity {get;set;}

        new void Update(GameTime gameTime);
        new void Draw(SpriteBatch spriteBatch);
        void LandOnBlock(float blockTopY);
    }
}