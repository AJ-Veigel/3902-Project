using Microsoft.Xna.Framework;
using System.Collections.Generic;
using SprintZero.blocks;
using SprintZero.Marios;
using System;


namespace SprintZero.PBCollision
{
    public static class playerBlockCollision
    {
   public static void checkBlockCollision(IMario mario, List<IBlock> allBlocks)
{
   
    Rectangle mariowithExtraBound = mario.MarioCollider;
    const int theBound = 16;
    mariowithExtraBound.Inflate(theBound, theBound);

    //this part is to ensure that mario is going to walk and not have a broken animation while walking on the ground
    List<IBlock> blocks = new List<IBlock>();
    foreach (var block in allBlocks)
    {
        if (block.Collider.Intersects(mariowithExtraBound))
            blocks.Add(block);
    }

    Console.WriteLine($"Blocks checked: {blocks.Count}");

    CollisionSide theSide;
    bool standingOnBlock = false;
    float highestBlockTop = float.MaxValue;
    const int tolerance = 8;

    foreach (IBlock block in blocks)
    {
        Rectangle marioRect = mario.MarioCollider;
        Rectangle blockRect = block.Collider;

        if (marioRect.Intersects(blockRect))
        {
            theSide = getCollisionSide(marioRect, blockRect);
           Console.WriteLine($"[Collision Debug] mario collided with block at {blockRect.Location} on {theSide} side");
            block.onCollision(mario, theSide);

            if (theSide == CollisionSide.Top)
            {
                standingOnBlock = true;
                if (blockRect.Top < highestBlockTop)
                    highestBlockTop = blockRect.Top;
            }
        }

  
        bool withinX = marioRect.Right > blockRect.Left && marioRect.Left < blockRect.Right;
        bool nearTop = marioRect.Bottom >= blockRect.Top - tolerance && marioRect.Bottom <= blockRect.Top + tolerance;

        if (withinX && nearTop && mario.yVelocity >= 0)
        {
            standingOnBlock = true;
            if (blockRect.Top < highestBlockTop)
                highestBlockTop = blockRect.Top;
        }

        if (standingOnBlock)
        {
            if (!mario.isOnGround || mario.yVelocity > 0)
                mario.LandOnBlock(highestBlockTop);

            mario.isOnGround = true;
            mario.Falling = false;
            mario.Jumping = false;
        }else{

            bool blockUnderMario = false;
            foreach (var b in blocks)
            {
                if (marioRect.Bottom >= b.Collider.Top - tolerance && marioRect.Bottom <= b.Collider.Top + tolerance &&
                    marioRect.Right > b.Collider.Left && marioRect.Left < b.Collider.Right)
                {
                    blockUnderMario = true;
                    break;
                }
            }

            if (!blockUnderMario && mario.yVelocity >= 0)
            {
                mario.isOnGround = false;
                mario.Falling = true;
            }
        }
    }
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