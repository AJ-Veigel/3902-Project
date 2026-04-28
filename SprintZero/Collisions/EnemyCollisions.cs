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

            if (currentMario.Invincible)
                return;

            Rectangle mRect = currentMario.MarioCollider;
            Rectangle eRect = currentEnemy.EnemyCollider;


            bool isAbove = mRect.Bottom <= eRect.Center.Y + 16;

            if (isAbove)
            {
                if (currentMario.yVelocity > 0) //using currentMario.Falling caused the koopa shell to automatically kick out 
                {
                    if (currentEnemy is Koopa koopa && (koopa.KoopaState == Koopa.KoopaStates.ShellStill || koopa.KoopaState == Koopa.KoopaStates.ShellStill2))
                    {
                        bool kickRight = mRect.Center.X < eRect.Center.X;
                        koopa.Kicked(kickRight);
                        ScoreManager.KickShell(game);
                    }
                    else
                    {
                        currentEnemy.Stomped();
                        ScoreManager.EnemyStomped(game);
                    }

                    currentMario.Bounce();
                }
            }
            else
            {
                if (currentEnemy is Koopa koopa && (koopa.KoopaState == Koopa.KoopaStates.ShellStill || koopa.KoopaState == Koopa.KoopaStates.ShellStill2))
                {
                    bool kickRight = mRect.Center.X < eRect.Center.X;
                    koopa.Kicked(kickRight);
                    ScoreManager.KickShell(game);
                }
                else
                {
                    Damage();
                }
            }
        }

        public static void CheckEnemyEnemyCollisions(List<IEnemy> enemies, Game1 game)
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

                        //booleans for determining koopa shell kills
                        bool thisKiller = thisEnemy.ActionState == EnemyAction.Kill;
                        bool otherKiller = otherEnemy.ActionState == EnemyAction.Kill;
                        bool thisAlive = !thisEnemy.Dead;
                        bool otherAlive = !otherEnemy.Dead;


                        thisEnemy.CollideWithEnemy(otherEnemy);
                        otherEnemy.CollideWithEnemy(thisEnemy);

                        //updates score for koopa shell kills
                        if (thisKiller && otherAlive && otherEnemy.Dead)
                        {
                            ScoreManager.EnemyDefeatedByShell(game);
                        }
                        else if (otherKiller && thisAlive && thisEnemy.Dead)
                        {
                            ScoreManager.EnemyDefeatedByShell(game);
                        }
                    }
                }
            }
        }

        public static void CheckEnemyBlockCollisions(IEnemy currentEnemy, List<IBlock> blocks, TileMap map)
        {
            if (currentEnemy != null && !currentEnemy.Dead && !(currentEnemy is BarFireball))
            {
                List<IBlock> nearbyBlocks = map.getBlocksInRectangle(currentEnemy.EnemyCollider, 64);
                nearbyBlocks.AddRange(blocks);

                foreach (var block in nearbyBlocks)
                {
                    Rectangle blockRect = block.Collider;
                    Rectangle enemyRect = currentEnemy.EnemyCollider;

                    if (enemyRect.Intersects(blockRect))
                    {
                        float overlapX = Math.Min(enemyRect.Right, blockRect.Right) - Math.Max(enemyRect.Left, blockRect.Left);
                        float overlapY = Math.Min(enemyRect.Bottom, blockRect.Bottom) - Math.Max(enemyRect.Top, blockRect.Top);

                        //side collision
                        if (overlapX < overlapY)
                        {
                            if (enemyRect.Center.X < blockRect.Center.X)
                                currentEnemy.position = new Vector2(currentEnemy.position.X - overlapX, currentEnemy.position.Y);
                            else
                                currentEnemy.position = new Vector2(currentEnemy.position.X + overlapX, currentEnemy.position.Y);

                            currentEnemy.ReverseDirection();
                        }
                        //top/bottom collision
                        else
                        {
                            if (enemyRect.Center.Y < blockRect.Center.Y)
                            {
                                currentEnemy.position = new Vector2(currentEnemy.position.X, currentEnemy.position.Y - overlapY);
                                currentEnemy.VelocityY = 0;
                                currentEnemy.onGround = true;
                            }
                            else
                            {
                                currentEnemy.position = new Vector2(currentEnemy.position.X, currentEnemy.position.Y + overlapY);
                                currentEnemy.VelocityY = 0;
                            }
                        }
                    }

                    currentEnemy.EnemyCollider = new Rectangle(
                        (int)currentEnemy.position.X,
                        (int)currentEnemy.position.Y,
                        enemyRect.Width,
                        enemyRect.Height
                    );
                }
            }
        }

        public static void CheckEnemyPipeCollisions(IEnemy currentEnemy, List<IPipe> pipes, TileMap map)
        {
            if (currentEnemy != null && !currentEnemy.Dead)
            {
                List<IPipe> nearbyPipes = map.getPipesInRectangle(currentEnemy.EnemyCollider, 64);
                nearbyPipes.AddRange(pipes);

                foreach (var pipe in nearbyPipes)
                {
                    Rectangle pipeRect = pipe.Collider;
                    Rectangle enemyRect = currentEnemy.EnemyCollider;

                    if (enemyRect.Intersects(pipeRect))
                    {
                        float overlapX = Math.Min(enemyRect.Right, pipeRect.Right) - Math.Max(enemyRect.Left, pipeRect.Left);
                        float overlapY = Math.Min(enemyRect.Bottom, pipeRect.Bottom) - Math.Max(enemyRect.Top, pipeRect.Top);

                        // Side collision
                        if (overlapX < overlapY)
                        {
                            if (enemyRect.Center.X < pipeRect.Center.X)
                                currentEnemy.position = new Vector2(currentEnemy.position.X - overlapX, currentEnemy.position.Y);
                            else
                                currentEnemy.position = new Vector2(currentEnemy.position.X + overlapX, currentEnemy.position.Y);

                            currentEnemy.ReverseDirection();
                        }
                        // Top/bottom collision
                        else
                        {
                            if (enemyRect.Center.Y < pipeRect.Center.Y)
                            {
                                currentEnemy.position = new Vector2(currentEnemy.position.X, currentEnemy.position.Y - overlapY);
                                currentEnemy.VelocityY = 0;
                                currentEnemy.onGround = true;
                            }
                            else
                            {
                                currentEnemy.position = new Vector2(currentEnemy.position.X, currentEnemy.position.Y + overlapY);
                                currentEnemy.VelocityY = 0;
                            }
                        }
                    }

                    currentEnemy.EnemyCollider = new Rectangle(
                        (int)currentEnemy.position.X,
                        (int)currentEnemy.position.Y,
                        enemyRect.Width,
                        enemyRect.Height
                    );
                }
            }
        }
    }
}