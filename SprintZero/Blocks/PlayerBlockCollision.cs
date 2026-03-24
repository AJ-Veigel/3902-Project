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

            foreach (IBlock block in blocks)
            {
                if (mario.MarioCollider.Intersects(block.Collider))
                {
                    CollisionSide theSide = getCollisionSide(mario.MarioCollider, block.Collider);
                    block.onCollision(mario, theSide);
                }
            }
        }
        private static CollisionSide getCollisionSide(Rectangle mario, Rectangle block)
        {
            CollisionSide theSide = CollisionSide.None;
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