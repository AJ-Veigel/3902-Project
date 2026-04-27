using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero.Marios;

namespace SprintZero.blocks
{
    public interface IPipe
    {
        int levelNum { get; set; }
        int bonus { get; set; }
        Vector2 location { get; set; }
        Vector2 marioSpawnPos { get; set; }
        Rectangle Collider { get; set; }
        void onCollision(IMario mario, CollisionSide side);
        void onCollision(IMario mario, CollisionSide side, Game1 game);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}