using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteZero.Marios
{
    public interface IMario
    {
        
        Vector2 position {get;set;}
        Boolean Direction {get;set;}
        Boolean Jumping {get; set;}
        Boolean Sprint {get;set;}
        Boolean Swim {get;set;}
        Boolean Crouch {get;set;}
        void Move();
        void StopMove();
        void Jump();
        void Fireball();

        
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}