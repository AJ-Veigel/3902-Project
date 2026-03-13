using Microsoft.Xna.Framework;
using System.Collections.Generic;
using SprintZero.blocks;
using SprintZero.Marios;

namespace SprintZero.PBCollision
{
    public static class playerBlockCollision
    {
        public static void checkBlockCollision(IMario mario, List<IBlock> blocks)
        {
            Vector2 zero = new Vector2(0, 0);
            Rectangle marioRect = mario.Collider.getBoundingRectangle(zero);
            foreach (IBlock block in blocks)
            {
                if (marioRect.Intersects(block.Collider))
                {
                    CollisionSide theSide = getCollisionSide(marioRect, block.Collider);
                    block.onCollision(mario, theSide);
                }
                else
                { 
                    bool horizontallyAbove = marioRect.Right > block.Collider.Left &&
                                             marioRect.Left < block.Collider.Right;

                    bool onTop = marioRect.Bottom >= block.Collider.Top - 1 &&
                                 marioRect.Bottom <= block.Collider.Top + 5;

                    if (horizontallyAbove && !onTop)
                    {
                        
                        mario.Falling = true;
                    }
                }
            }
        }
        private static CollisionSide getCollisionSide(Rectangle mario,Rectangle block)
        {
            CollisionSide theSide = CollisionSide.Top;
            Rectangle overlap = Rectangle.Intersect(mario,block);
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