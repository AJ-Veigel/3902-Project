using SprintZero.Marios;
using SprintZero.Items;
using System;
using SoundManager;
using System.Collections.Generic;
using SprintZero.blocks;
using SprintZero.Map;
using Microsoft.Xna.Framework;

namespace ItemCollisions
{
    public static class ItemCollision
    {
        // Handles enemy-Mario interactions
        public static void CheckItemMarioCollisions(ICollectable currentItem, IMario currentMario)
        {
            if (currentItem.RectCollider.Intersects(currentMario.MarioCollider) && !currentItem.Collected)
            {
                if (currentMario.Falling && currentMario.MarioCollider.Bottom <= currentItem.RectCollider.Center.Y + 10)
                {
                    if (currentItem is Mushroom mushroom)
                    {
                        mushroom.Collected = true;
                        Music.itemSound.Play();
                    }
                    else if(currentItem is Flower flower)
                    {
                        flower.Collected = true;
                        Music.itemSound.Play();
                    }
                    else if(currentItem is Coin coin)
                    {
                        coin.Collected = true;
                        Music.itemSound.Play();
                    }
                    else if(currentItem is OneUp oneUp)
                    {
                        oneUp.Collected = true;
                        Music.itemSound.Play();
                    }
                    else if(currentItem is Star star)
                    {
                        star.Collected = true;
                        Music.itemSound.Play();
                    }
                }
                else
                {

                }
            }
        }


        public static void CheckItemBlockCollisions(ICollectable currentItem, List<IBlock> blocks, TileMap map)
        {
            if (currentItem != null)
            {
                List<IBlock> nearbyBlocks = map.getBlocksInRectangle(currentItem.RectCollider);
                nearbyBlocks.AddRange(blocks);

                foreach (var block in nearbyBlocks)
                {
                    Rectangle blockRect = block.Collider;
                    Rectangle enemyRect = currentItem.RectCollider;

                    if (enemyRect.Intersects(blockRect))
                    {
                        float overlapX = Math.Min(enemyRect.Right, blockRect.Right) - Math.Max(enemyRect.Left, blockRect.Left);
                        float overlapY = Math.Min(enemyRect.Bottom, blockRect.Bottom) - Math.Max(enemyRect.Top, blockRect.Top);

                        // Side collision
                        if (overlapX < overlapY)
                        {
                            if (enemyRect.Center.X < blockRect.Center.X)
                                currentItem.location = new Vector2(currentItem.location.X - overlapX, currentItem.location.Y);
                            else
                                currentItem.location = new Vector2(currentItem.location.X + overlapX, currentItem.location.Y);

                            currentItem.ReverseDirection();
                        }
                        // Top/bottom collision
                        else
                        {
                            if (enemyRect.Center.Y < blockRect.Center.Y)
                            {
                                currentItem.location = new Vector2(currentItem.location.X, currentItem.location.Y - overlapY);
                                currentItem.VelocityY = 0;
                                currentItem.onGround = true;
                            }
                            else
                            {
                                currentItem.location = new Vector2(currentItem.location.X, currentItem.location.Y + overlapY);
                                currentItem.VelocityY = 0;
                            }
                        }
                    }

                    currentItem.RectCollider = new Rectangle(
                        (int)currentItem.location.X,
                        (int)currentItem.location.Y,
                        enemyRect.Width,
                        enemyRect.Height
                    );
                }
            }
        }
    }
}