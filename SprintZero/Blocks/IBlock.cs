using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero.Marios;

namespace SprintZero.blocks
{
    public enum CollisionSide { None, Top, Bottom, Left, Right }
    public interface IBlock
    {
        Vector2 location { get; set; }
        Rectangle Collider { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);

        void  onCollision(IMario mario, CollisionSide side){}
    }
}