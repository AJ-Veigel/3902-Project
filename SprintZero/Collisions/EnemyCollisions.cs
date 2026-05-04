using SprintZero.Marios;
using SpriteZero.Enemies;
using System;
using System.Collections.Generic;
using SprintZero.blocks;
using SprintZero.Map;
using Microsoft.Xna.Framework;
using SprintZero;

namespace EnemyCollisions
{
    public static class CheckEnemyCollisions
    {
        public enum EnemyAction { None, Bounce, Kill }

        public static void CheckEnemyMarioCollisions(IEnemy currentEnemy, IMario currentMario, Action Damage, Game1 game)
        {
            if (currentEnemy == null || currentEnemy.Dead || !currentEnemy.EnemyCollider.Intersects(currentMario.MarioCollider))
                return;

            if (currentMario.IsStarPower)
            {
                currentEnemy.Dead = true;
                ScoreManager.EnemyStomped(game);
                SoundManager.Music.blockSound.Play();
                return;
            }

            if (currentMario.Invincible) return;

            Rectangle marioRect = currentMario.MarioCollider;
            Rectangle enemyRect = currentEnemy.EnemyCollider;

            bool isAbove = marioRect.Bottom <= enemyRect.Center.Y + 16;

            //each enemy decides how it reacts to Mario
            currentEnemy.HandleMarioCollision(currentMario, isAbove, Damage, game);
        }

        //same loop from previous builds, but we use helper methods to split up the logic
        public static void CheckEnemyEnemyCollisions(List<IEnemy> enemies, Game1 game)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                IEnemy thisEnemy = enemies[i];
                if (!IsActive(thisEnemy)) continue; 

                for (int j = i + 1; j < enemies.Count; j++)
                {
                    IEnemy otherEnemy = enemies[j];
                    if (!IsActive(otherEnemy)) continue; 

                    if (thisEnemy.EnemyCollider.Intersects(otherEnemy.EnemyCollider))
                    {
                        ResolveEnemyInteraction(thisEnemy, otherEnemy, game);
                    }
                }
            }
        }

        //checks if enemy is alive and active
        private static bool IsActive(IEnemy enemy)
        {
            return enemy != null && !enemy.Dead && !enemy.Despawn;
        }

        //handles the actual enemy interaction logic
        private static void ResolveEnemyInteraction(IEnemy enemy1, IEnemy enemy2, Game1 game)
        {
            if (enemy1.ActionState == EnemyAction.Bounce && enemy2.ActionState == EnemyAction.Bounce)
            {
                ApplyBouncePhysics(enemy1, enemy2);
            }

            bool e1WasKiller = enemy1.ActionState == EnemyAction.Kill;
            bool e2WasKiller = enemy2.ActionState == EnemyAction.Kill;

            enemy1.CollideWithEnemy(enemy2);
            enemy2.CollideWithEnemy(enemy1);

            if ((e1WasKiller && enemy2.Dead) || (e2WasKiller && enemy1.Dead))
            {
                ScoreManager.EnemyDefeatedByShell(game);
            }
        }

        //helper to make enemies bounce off each other if it's not a Koopa shell
        private static void ApplyBouncePhysics(IEnemy enemy1, IEnemy enemy2)
        {
            Rectangle rect1 = enemy1.EnemyCollider;
            Rectangle rect2 = enemy2.EnemyCollider;

            float overlapX = Math.Min(rect1.Right, rect2.Right) - Math.Max(rect1.Left, rect2.Left);

            float direction = rect1.Center.X < rect2.Center.X ? -1 : 1;

            enemy1.ResolveTerrainCollision(direction * (overlapX / 2), 0);
            enemy2.ResolveTerrainCollision(-direction * (overlapX / 2), 0);
        }

        public static void CheckEnemyBlockCollisions(IEnemy currentEnemy, List<IBlock> blocks, TileMap map)
        {
            if (currentEnemy == null || currentEnemy.Dead) return;

            List<IBlock> nearbyBlocks = map.getBlocksInRectangle(currentEnemy.EnemyCollider, 64);
            nearbyBlocks.AddRange(blocks);

            foreach (var block in nearbyBlocks)
            {
                HandleStaticCollision(currentEnemy, block.Collider);
            }
        }

        public static void CheckEnemyPipeCollisions(IEnemy currentEnemy, List<IPipe> pipes, TileMap map)
        {
            if (currentEnemy == null || currentEnemy.Dead) return;

            List<IPipe> nearbyPipes = map.getPipesInRectangle(currentEnemy.EnemyCollider, 64);
            nearbyPipes.AddRange(pipes);

            foreach (var pipe in nearbyPipes)
            {
                HandleStaticCollision(currentEnemy, pipe.Collider);
            }
        }

        
        private static void HandleStaticCollision(IEnemy currentEnemy, Rectangle staticRect)
        {
            Rectangle enemyRect = currentEnemy.EnemyCollider;

            if (enemyRect.Intersects(staticRect))
            {
                float overlapX = Math.Min(enemyRect.Right, staticRect.Right) - Math.Max(enemyRect.Left, staticRect.Left);
                float overlapY = Math.Min(enemyRect.Bottom, staticRect.Bottom) - Math.Max(enemyRect.Top, staticRect.Top);

                if (overlapX < overlapY) 
                {
                    float sign = enemyRect.Center.X < staticRect.Center.X ? -1 : 1;
                    currentEnemy.ResolveTerrainCollision(sign * overlapX, 0);
                    currentEnemy.ReverseDirection();
                }
                else 
                {
                    float sign = enemyRect.Center.Y < staticRect.Center.Y ? -1 : 1;
                    currentEnemy.ResolveTerrainCollision(0, sign * overlapY);
                }
            }
        }
    }
}