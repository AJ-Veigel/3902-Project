using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteZero.Enemies
{
    public interface IEnemy
    {

        Vector2 position { get; set; }
        Boolean Dead { get; set; }


        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}