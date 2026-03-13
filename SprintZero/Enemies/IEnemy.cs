using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SprintZero.Enemies
{
    public interface IEnemy
    {

        Vector2 position { get; set; }
        Boolean Dead { get; set; }
        Rectangle EnemyCollider { get; set; }
        Boolean GetCollidable();

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}