using System;
using System.Collections.Generic;
using SprintZero.blocks;
using SprintZero.Enemies;
using SpriteZero.Enemies;

namespace FireballCollisions
{
    public class FireballCollision
    {
        public static void checkFireballBlockCollision(Fireball currentFireball, List<IBlock> blocks) 
        {
            foreach (IBlock block in blocks)
            {
                if (currentFireball.FireballCollider.Intersects(block.Collider))
                {
                    int overlapX = Math.Min(currentFireball.FireballCollider.Right, block.Collider.Right)
                    - Math.Max(currentFireball.FireballCollider.Left, block.Collider.Left);
                    int overlapY = Math.Min(currentFireball.FireballCollider.Bottom, block.Collider.Bottom)
                    - Math.Max(currentFireball.FireballCollider.Top, block.Collider.Top);

                    // Fireball only pops if it hits side of block, continue bouncing otherwise.
                    if (overlapX < overlapY) 
                    {
                        currentFireball.Pop();
                    }
                    else
                    {
                        bool hitFromAbove = currentFireball.FireballCollider.Center.Y < block.Collider.Center.Y;
                        if (hitFromAbove)
                            currentFireball.Bounce(block.Collider.Top);
                        else
                            currentFireball.Pop();
                    }
                }
            }
        }

        public static void checkFireballPipeCollision(Fireball currentFireball, List<IPipe> pipes) 
        {
            foreach (IPipe pipe in pipes)
            {
                if (currentFireball.FireballCollider.Intersects(pipe.Collider))
                {
                    int overlapX = Math.Min(currentFireball.FireballCollider.Right, pipe.Collider.Right)
                    - Math.Max(currentFireball.FireballCollider.Left, pipe.Collider.Left);
                    int overlapY = Math.Min(currentFireball.FireballCollider.Bottom, pipe.Collider.Bottom)
                    - Math.Max(currentFireball.FireballCollider.Top, pipe.Collider.Top);

                    // Fireball only pops if it hits side of pipe, continue bouncing otherwise.
                    if (overlapX < overlapY) 
                    {
                        currentFireball.Pop();
                    }
                    else
                    {
                        bool hitFromAbove = currentFireball.FireballCollider.Center.Y < pipe.Collider.Center.Y;
                        if (hitFromAbove)
                            currentFireball.Bounce(pipe.Collider.Top);
                        else
                            currentFireball.Pop();
                    }
                }
            }
        }
        
        // I think it would make most sense to check the fireball against every enemy on the map, because
        // There will never be a time where there are so many enemies loaded that it would lag the game,
        // Or really alter the games performance in any way, since we are planning on loading the enemies
        // Into the list as the player progresses through the levels.
        public static void checkFireballEnemyCollision(Fireball currentFireball, List<IEnemy> enemies) 
        {
            foreach (IEnemy enemy in enemies)
            {
                if (currentFireball.FireballCollider.Intersects(enemy.EnemyCollider))
                {
                    if (enemy is Bowser bowser)
                    {
                        bowser.TakeDamage();

                    }
                    else
                    {
                        enemy.Dead = true;
                    }
                    currentFireball.Pop();
                }
            }
        }
    }
}