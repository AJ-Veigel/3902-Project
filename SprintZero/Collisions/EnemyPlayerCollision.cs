using SprintZero.Marios;
using SpriteZero.Enemies;
using System;
using System.Collections.Generic;
using SprintZero.blocks;
using SprintZero.Map;
using Microsoft.Xna.Framework;

namespace EnemyPlayerCollision
{
    public static class CheckEnemyCollisions
    {
        // Handles enemy-Mario interactions
        public static void CheckEnemyMarioCollisions(IEnemy currentEnemy, IMario currentMario, Action Damage)
        {
            if (currentEnemy.EnemyCollider.Intersects(currentMario.MarioCollider) && !currentEnemy.Dead)
            {
                if (currentMario.Falling && currentMario.MarioCollider.Bottom <= currentEnemy.EnemyCollider.Center.Y + 10)
                {
                    if (currentEnemy is Koopa koopa && (koopa.KoopaState == Koopa.KoopaStates.ShellStill || koopa.KoopaState == Koopa.KoopaStates.ShellStill2))
                    {
                        bool kickRight = currentMario.MarioCollider.Center.X < koopa.EnemyCollider.Center.X;
                        koopa.Kicked(kickRight);
                    }
                    else
                    {
                        currentEnemy.Stomped();
                    }

                    currentMario.Bounce();
                }
                else
                {
                    if (currentEnemy is Koopa koopa && (koopa.KoopaState == Koopa.KoopaStates.ShellStill || koopa.KoopaState == Koopa.KoopaStates.ShellStill2))
                    {
                        bool kickRight = currentMario.MarioCollider.Center.X < koopa.EnemyCollider.Center.X;
                        koopa.Kicked(kickRight);
                    }
                    else
                    {
                        Damage();
                    }
                }
            }
        }


        public static void CheckEnemyBlockCollisions(IEnemy currentEnemy, List<IBlock> blocks, TileMap map)
        {
            if (currentEnemy != null && !currentEnemy.Dead)
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

                        // Side collision
                        if (overlapX < overlapY)
                        {
                            if (enemyRect.Center.X < blockRect.Center.X)
                                currentEnemy.position = new Vector2(currentEnemy.position.X - overlapX, currentEnemy.position.Y);
                            else
                                currentEnemy.position = new Vector2(currentEnemy.position.X + overlapX, currentEnemy.position.Y);

                            currentEnemy.ReverseDirection();
                        }
                        // Top/bottom collision
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
    }
}