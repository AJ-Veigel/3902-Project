using Microsoft.Xna.Framework;
using System.Collections.Generic;
using SprintZero.blocks;
using SprintZero.Marios;

using System;
using System.IO.Pipelines;


namespace SprintZero.PBCollision
{
    public static class playerBlockCollision
    {

        public static void checkCameraCollision(IMario mario, Rectangle bounds, Action<int> setMario, Action Damage)
        {
            if (mario.location.X < bounds.Left)
            {
                mario.location = new Vector2(bounds.Left, mario.location.Y);
                if (mario.xVelocity < 0)
                {
                    mario.xVelocity = 0.0f;
                }
            }
            if (mario.location.Y > bounds.Bottom)
            {
                setMario(0);
                Damage();
            }
        }
        public static int checkBlockCollision(IMario mario, List<IBlock> allBlocks, List<IPipe> allPipes, Game1 game)
        {
            int mapChange = 0;
            Rectangle mariowithExtraBound = mario.MarioCollider;
            const int theBound = 16;
            mariowithExtraBound.Inflate(theBound, theBound);
            bool handleBySpecial = false;


            List<IBlock> blocks = new List<IBlock>();
            List<IPipe> pipes = new List<IPipe>();
            foreach (var block in allBlocks)
            {
                if (block.Collider.Intersects(mariowithExtraBound))
                    blocks.Add(block);
            }
            foreach (var block in allPipes)
            {
                if (block.Collider.Intersects(mariowithExtraBound))
                    pipes.Add(block);
            }
            if (mario.SlidingFlag)
            {

                foreach (var block in allBlocks)
                {
                    if (block is FlagMiddle || block is FlagBase)
                    {
                        block.onCollision(mario, CollisionSide.None);
                    }
                }

                return mapChange;
            }
            CollisionSide theSide;
            bool standingOnBlock = false;
            float highestBlockTop = float.MaxValue;
            const int tolerance = 8;

            foreach (IBlock block in blocks)
            {
                if (block is CastleBlock || block is Axe)
                {
                    mario.WinState = true;
                    mario.xVelocity = 0f;
                    mario.yVelocity = 0f;
                    mario.StopMove();
                    return mapChange;
                }

                Rectangle marioRect = mario.MarioCollider;
                Rectangle blockRect = block.Collider;

                if (marioRect.Intersects(blockRect))
                {
                    theSide = getCollisionSide(marioRect, blockRect);
                    Console.WriteLine($"[Collision Debug] mario collided with block at {blockRect.Location} on {theSide} side");
                    if (block is FlagMove || block is HiddenBlock)
                    {
                        block.onCollision(mario, theSide);
                        handleBySpecial = true;
                    }
                    else
                    {
                        block.onCollision(mario, theSide);
                    }
                    if (!handleBySpecial)
                    {
                        if (theSide == CollisionSide.Top && !mario.Jumping)
                        {
                            standingOnBlock = true;
                            if (blockRect.Top < highestBlockTop)
                                highestBlockTop = blockRect.Top;
                        }
                    }
                }

                bool withinX = marioRect.Right > blockRect.Left && marioRect.Left < blockRect.Right;
                bool nearTop = marioRect.Bottom >= blockRect.Top - tolerance && marioRect.Bottom <= blockRect.Top + tolerance;

                HiddenBlock hit = null;
                if (block is HiddenBlock)
                {
                    hit = (HiddenBlock)block;
                }

                if (withinX && nearTop && mario.yVelocity >= 0 && hit != null)
                {
                    if (hit.GetisHit())
                    {
                        standingOnBlock = true;
                        if (blockRect.Top < highestBlockTop)
                            highestBlockTop = blockRect.Top;
                    }
                }
                else if (withinX && nearTop && mario.yVelocity >= 0 && (!(block is FlagMove) || !(block is FlagTop) || !(block is FlagMiddle)))
                {
                    standingOnBlock = true;
                    if (blockRect.Top < highestBlockTop)
                        highestBlockTop = blockRect.Top;
                }
            }
            foreach (IPipe pipe in pipes)
            {
                Rectangle marioRect = mario.MarioCollider;
                Rectangle pipeRect = pipe.Collider;

                if (marioRect.Intersects(pipeRect))
                {
                    theSide = getCollisionSide(marioRect, pipeRect);
                    Console.WriteLine($"[Collision Debug] mario collided with block at {pipeRect.Location} on {theSide} side");

                    pipe.onCollision(mario, theSide, game);

                }

                bool aboveTubeTop = marioRect.Right < pipeRect.Right && marioRect.Left > pipeRect.Left;
                bool withinX = marioRect.Right > pipeRect.Left && marioRect.Left < pipeRect.Right;
                bool nearTop = marioRect.Bottom >= pipeRect.Top - tolerance && marioRect.Bottom <= pipeRect.Top + tolerance;

                if (withinX && nearTop && mario.yVelocity >= 0)
                {
                    if (pipe is TubeTop && pipe.levelNum > 0 && aboveTubeTop)
                    {
                        pipe.onCollision(mario, CollisionSide.Top, game);
                        mapChange++;
                    }
                    standingOnBlock = true;
                    if (pipeRect.Top < highestBlockTop)
                        highestBlockTop = pipeRect.Top;
                }
            }
            if (!handleBySpecial && !mario.SlidingFlag)
            {
                if (standingOnBlock)
                {
                    if (!mario.isOnGround || mario.yVelocity > 0)
                        mario.LandOnBlock(highestBlockTop);

                    mario.isOnGround = true;
                    mario.Falling = false;
                    mario.Jumping = false;
                }
                else
                {
                    mario.isOnGround = false;
                    mario.Falling = true;
                }
            }
            return mapChange;
        }

        private static CollisionSide getCollisionSide(Rectangle mario, Rectangle block)
        {
            CollisionSide theSide;
            Rectangle overlap = Rectangle.Intersect(mario, block);
            if (overlap.Width < overlap.Height)
            {
                if (mario.Center.X < block.Center.X)
                {
                    theSide = CollisionSide.Left;
                }
                else
                {
                    theSide = CollisionSide.Right;
                }
            }
            else
            {
                if (mario.Center.Y < block.Center.Y)
                {
                    theSide = CollisionSide.Top;
                }
                else
                {
                    theSide = CollisionSide.Bottom;
                }
            }
            return theSide;
        }
    }
}