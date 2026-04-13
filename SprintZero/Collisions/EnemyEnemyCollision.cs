using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
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

                if (thisEnemy.Dead || thisEnemy.Despawn) continue;

                for (int j = i + 1; j < enemies.Count; j++) 
                {
                    IEnemy otherEnemy = enemies[j];
                    Rectangle rect1 = thisEnemy.EnemyCollider;
                    Rectangle rect2 = otherEnemy.EnemyCollider;

                    if (rect1.Intersects(rect2))
                    {
                        if (thisEnemy.ActionState == EnemyAction.Bounce && otherEnemy.ActionState == EnemyAction.Bounce)
                        {
                            float overlapX = Math.Min(rect1.Right, rect2.Right) - Math.Max(rect1.Left, rect2.Left);

                            if (rect1.Center.X < rect2.Center.X)
                            {
                                thisEnemy.position = new Vector2(thisEnemy.position.X - (overlapX / 2), thisEnemy.position.Y);
                                otherEnemy.position = new Vector2(otherEnemy.position.X + (overlapX / 2), otherEnemy.position.Y);
                            }
                            else
                            {
                                thisEnemy.position = new Vector2(thisEnemy.position.X + (overlapX / 2), thisEnemy.position.Y);
                                otherEnemy.position = new Vector2(otherEnemy.position.X - (overlapX / 2), otherEnemy.position.Y);
                            }

                            thisEnemy.EnemyCollider = new Rectangle((int)thisEnemy.position.X, (int)thisEnemy.position.Y, rect1.Width, rect1.Height);
                            otherEnemy.EnemyCollider = new Rectangle((int)otherEnemy.position.X, (int)otherEnemy.position.Y, rect2.Width, rect2.Height);
                        }

                        thisEnemy.CollideWithEnemy(otherEnemy);
                        otherEnemy.CollideWithEnemy(thisEnemy);
                    }
                }
            }
        }
    }
}
