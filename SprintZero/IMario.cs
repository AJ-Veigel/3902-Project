using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteZero.Marios
{
    public interface IMario
    {
        
        Vector2 position {get;set;}
        Boolean Sprint {get;set;}
        Boolean Swim {get;set;}
        Boolean Crouch {get;set;}
        void MoveRight();
        void MoveLeft();
        void StopRight();
        void StopLeft();
        void Jump();
        void Fireball();

        
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}