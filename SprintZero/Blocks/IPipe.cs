using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero.Marios;

namespace SprintZero.blocks
{
    public interface IPipe
    {
        Vector2 location { get; set; }
        Rectangle Collider { get; set; }
        void onCollision(IMario mario, CollisionSide side);
        void onCollision(IMario mario, CollisionSide side, Game1 game);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}