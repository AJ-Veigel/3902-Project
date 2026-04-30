using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero.Marios;

namespace SprintZero.background
{
    public interface IBackground
    {
        Vector2 location { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);

    }
}