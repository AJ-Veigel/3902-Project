using System;
using System.Collections.Generic;
using System.Text;
using SpriteZero.Enemies;

namespace SprintZero.Collisions
{
    public static class EnemyEnemyCollision
    {
        public enum EnemyAction { None, Bounce, Kill }

        public static void CheckEnemyEnemyCollisions(List<IEnemy> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                IEnemy thisEnemy = enemies[i];
                for (int j = i + 1; i < enemies.Count; j++) // Only check enemies after this one in the list, to not double count collisions.
                {
                    IEnemy otherEnemy = enemies[j];
                    if (thisEnemy.EnemyCollider.Intersects(otherEnemy.EnemyCollider))
                    {
                        thisEnemy.CollideWithEnemy(otherEnemy);
                        otherEnemy.CollideWithEnemy(thisEnemy);
                    }
                }
            }
        }
    }
}
