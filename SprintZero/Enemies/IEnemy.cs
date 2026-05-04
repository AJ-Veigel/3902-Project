using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EnemyCollisions;

namespace SpriteZero.Enemies
{
    public interface IEnemy
    {

        Vector2 position { get; set; }
        Boolean Dead { get; set; }
        Boolean onGround { get; set; }
        Boolean Despawn { get; set; }
        Rectangle EnemyCollider { get; set; }
        float VelocityX { get; set; }
        float VelocityY { get; set; }

        CheckEnemyCollisions.EnemyAction ActionState { get; }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void ReverseDirection();
        void Stomped();
        void ResolveTerrainCollision(float deltaX, float deltaY);
        void HandleMarioCollision(SprintZero.Marios.IMario mario, bool isAbove, Action damageMario, SprintZero.Game1 game);

        void CollideWithEnemy(IEnemy enemy);
    }
}