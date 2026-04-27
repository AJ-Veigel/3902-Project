using SprintZero.Marios;
using SprintZero.Items;
using System;
using SoundManager;
using System.Collections.Generic;
using SprintZero.blocks;
using SprintZero.Map;
using Microsoft.Xna.Framework;
using SprintZero;

namespace ItemCollisions
{
    public static class ItemCollision
    {

        public static void CheckItemMarioCollisions(ICollectable currentItem, IMario currentMario, Game1 game)
        {
          
          System.Diagnostics.Debug.WriteLine("ItemCollision running: " + currentItem.GetType());
            if (currentItem.RectCollider.Intersects(currentMario.MarioCollider) && !currentItem.Collected)
            {
               if (currentItem.RectCollider.Intersects(currentMario.MarioCollider))
                {
                    if (currentItem is Mushroom mushroom)
                    {
                        mushroom.Collected = true;
                        if(game.currentMarioNum == 0) game.SetMario(1);
                        Music.itemSound.Play();
                        ScoreManager.CollectPowerUp(game);
                    }
                    else if(currentItem is Flower flower)
                    {
                        flower.Collected = true;
                        if(game.currentMarioNum <= 1) game.SetMario(2);
                        Music.itemSound.Play();
                        ScoreManager.CollectPowerUp(game);
                    }
                    else if(currentItem is Coin coin)
                    {
                        System.Diagnostics.Debug.WriteLine("COIN HIT DETECTED");
                        coin.Collected = true;
                        Music.coinSound.Play();
                        game.coinCount++;
                        ScoreManager.CollectCoin(game);
                        System.Diagnostics.Debug.WriteLine("Coin collected +200");
                    }
                    else if(currentItem is OneUp oneUp)
                    {
                        oneUp.Collected = true;
                        Music.oneupSound.Play();
                        game.livesCount++;
                    }
                    else if(currentItem is Star star)
                    {
                        star.Collected = true;
                        Music.itemSound.Play();
                        ScoreManager.CollectPowerUp(game);
                    }
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
                    Rectangle itemRect = currentItem.RectCollider;

                    if (itemRect.Intersects(blockRect) && currentItem.Collidable)
                    {
                        float overlapX = Math.Min(itemRect.Right, blockRect.Right) - Math.Max(itemRect.Left, blockRect.Left);
                        float overlapY = Math.Min(itemRect.Bottom, blockRect.Bottom) - Math.Max(itemRect.Top, blockRect.Top);

                        // Side collision
                        if (overlapX < overlapY)
                        {
                            if (itemRect.Center.X < blockRect.Center.X)
                                currentItem.location = new Vector2(currentItem.location.X - overlapX, currentItem.location.Y);
                            else
                                currentItem.location = new Vector2(currentItem.location.X + overlapX, currentItem.location.Y);

                            currentItem.ReverseDirection();
                        }
                        // Top/bottom collision
                        else
                        {
                            if (itemRect.Center.Y < blockRect.Center.Y)
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
                        itemRect.Width,
                        itemRect.Height
                    );
                }
            }
        }

        public static void CheckItemPipeCollisions(ICollectable currentItem, List<IPipe> pipes, TileMap map)
        {
            if (currentItem != null)
            {
                List<IPipe> nearbyPipes = map.getPipesInRectangle(currentItem.RectCollider);
                nearbyPipes.AddRange(pipes);

                foreach (var Pipe in nearbyPipes)
                {
                    Rectangle PipeRect = Pipe.Collider;
                    Rectangle itemRect = currentItem.RectCollider;

                    if (itemRect.Intersects(PipeRect) && currentItem.Collidable)
                    {
                        float overlapX = Math.Min(itemRect.Right, PipeRect.Right) - Math.Max(itemRect.Left, PipeRect.Left);
                        float overlapY = Math.Min(itemRect.Bottom, PipeRect.Bottom) - Math.Max(itemRect.Top, PipeRect.Top);

                        // Side collision
                        if (overlapX < overlapY)
                        {
                            if (itemRect.Center.X < PipeRect.Center.X)
                                currentItem.location = new Vector2(currentItem.location.X - overlapX, currentItem.location.Y);
                            else
                                currentItem.location = new Vector2(currentItem.location.X + overlapX, currentItem.location.Y);

                            currentItem.ReverseDirection();
                        }
                        // Top/bottom collision
                        else
                        {
                            if (itemRect.Center.Y < PipeRect.Center.Y)
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
                        itemRect.Width,
                        itemRect.Height
                    );
                }
            }
        }
    }
}