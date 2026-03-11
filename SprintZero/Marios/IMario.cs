using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteZero.Marios
{
    public interface IMario
    {
        
        Vector2 position {get;set;}
        Rectangle MarioCollider {get; set;}
        Boolean Direction {get;set;}
        Boolean Jumping {get; set;}
        Boolean Sprinting {get;set;}
        Boolean Swimming {get;set;}
        Boolean Crouching {get;set;}
        void Move();
        void StopMove();
        void Jump();
        void Crouch();
        void Fireball();
        void Damage();
        
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}