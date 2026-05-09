using SprintZero.Marios;
using Microsoft.Xna.Framework;

namespace SprintZero.blocks
{
    public static class DefaultBlockCollision
    {
        public static void DefaultCollision(IBlock block, IMario mario, CollisionSide side)
        {
            switch (side)
            {
                case CollisionSide.Left:
                    if (mario.xVelocity < 0) { break; }
                    mario.location = new Vector2(block.Collider.Left - mario.MarioCollider.Width, mario.location.Y);
                    mario.xVelocity = 0;
                    break;
                case CollisionSide.Right:
                    if (mario.xVelocity > 0) { break; }
                    mario.location = new Vector2(block.Collider.Right, mario.location.Y);
                    mario.xVelocity = 0;
                    break;
                case CollisionSide.Top:
                    break;
                case CollisionSide.Bottom:
                    if (mario.yVelocity > 0) { break; }
                    mario.location = new Vector2(mario.location.X, block.Collider.Bottom);
                    mario.yVelocity = 0;
                    break;
                default: throw new System.Exception("Invalid collision side for collision.");
            }
            return;
        }
    }
}