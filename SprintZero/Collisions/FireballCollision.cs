using System.Collections.Generic;
using SprintZero.blocks;
using SpriteZero.Enemies;

namespace FireballCollisions
{
    public class FireballCollision
    {
        private List<IEnemy> enemies;
        private int currentEnemyCount;
        private IEnemy currentEnemy;
        private List<IBlock> blocks;

        public FireballCollision(List<IEnemy> enemies, int currentEnemyCount, IEnemy currentEnemy, List<IBlock> blocks)
        {
            this.enemies = enemies;
            this.currentEnemyCount = currentEnemyCount;
            this.currentEnemy = currentEnemy;
            this.blocks = blocks;
        }

        public void collisionCheck(Fireball fb)
        {
            for (int j = enemies.Count - 1; j >= 0; j--)
            {
                if (currentEnemyCount == j && !currentEnemy.Dead)
                {
                    switch (enemies[j])
                    {
                        case Goomba:
                            if (fb.location.X < enemies[j].position.X + 16 &&
                                fb.location.X + 8 > enemies[j].position.X &&
                                fb.location.Y < enemies[j].position.Y + 16 &&
                                fb.location.Y + 8 > enemies[j].position.Y)
                            {
                                enemies[j].Dead = true;
                                fb.Pop();
                            }
                            break;
                        case Koopa:
                            if (fb.location.X < enemies[j].position.X + 16 &&
                                fb.location.X + 8 > enemies[j].position.X &&
                                fb.location.Y < enemies[j].position.Y + 24 &&
                                fb.location.Y + 8 > enemies[j].position.Y)
                            {
                                enemies[j].Dead = true;
                                fb.Pop();
                            }
                            break;
                    }
                }
            }


            for (int k = blocks.Count - 1; k >= 0; k--)
            {
                if (blocks[k] is MediumTube)
                {
                    if (fb.location.X < blocks[k].location.X + 120 &&
                        fb.location.X + 8 > blocks[k].location.X - 24 &&
                        fb.location.Y < blocks[k].location.Y + 192 &&
                        fb.location.Y + 8 > blocks[k].location.Y - 24)
                    {
                        fb.Pop();
                    }
                }
            }
        }

        public void UpdateCurrentEnemy(int enemyIndex, IEnemy enemy)
        {
            currentEnemyCount = enemyIndex;
            currentEnemy = enemy;
        }
    }
}