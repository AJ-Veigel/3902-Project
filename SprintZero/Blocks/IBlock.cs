using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteZero.Marios;

namespace SpriteZero.blocks
{
    public enum CollisionSide {None, Top, Bottom, Left, Right}
    public interface IBlock
    {
        
        Vector2 location {get;set;}
        Rectangle Collider {get; set;}
        Boolean GetCollidable();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);

        void onCollision(IMario mario, CollisionSide side);
    }
}