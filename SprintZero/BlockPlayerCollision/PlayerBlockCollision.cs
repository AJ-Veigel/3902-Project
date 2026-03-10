using Microsoft.Xna.Framework;
using System.Collections.Generic;
using SpriteZero.blocks;
using SpriteZero.Marios;

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
                    block.onHit(mario, theSide);
                }
                else
                {
                    
                    bool horizontallyAbove = mario.MarioCollider.Right > block.Collider.Left &&
                                             mario.MarioCollider.Left < block.Collider.Right;

                    bool onTop = mario.MarioCollider.Bottom >= block.Collider.Top - 1 &&
                                 mario.MarioCollider.Bottom <= block.Collider.Top + 4;

                    if (horizontallyAbove && !onTop)
                    {
                        
                        mario.Falling = true;
                        mario.position = new Vector2(mario.position.X, mario.position.Y + 2f); 
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