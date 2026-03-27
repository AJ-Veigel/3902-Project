using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SprintZero.Marios;

namespace SprintZero.blocks
{
    public enum CollisionSide { None, Top, Bottom, Left, Right }
    public interface IBlock
    {
        Vector2 location { get; set; }
        Rectangle Collider { get; set; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);

        void onCollision(IMario mario, CollisionSide side) // default implementation.
        {
            switch (side)
            {
                case CollisionSide.Left:
                    if (mario.xVelocity < 0) { break; }
                    mario.location = new Vector2(Collider.Left - mario.MarioCollider.Width, mario.location.Y);
                    mario.xVelocity = 0;
                    break;
                case CollisionSide.Right:
                    if (mario.xVelocity > 0) { break; }
                    mario.location = new Vector2(Collider.Right, mario.location.Y);
                    mario.xVelocity = 0;
                    break;
                case CollisionSide.Top:
                    if (mario.yVelocity < 0) { break; }

                    mario.LandOnBlock(Collider.Top);
                    break;
                case CollisionSide.Bottom:
                    if (mario.yVelocity > 0) { break; }
                    mario.location = new Vector2(mario.location.X, Collider.Bottom);
                    mario.yVelocity = 0;
                    break;
                default: throw new System.Exception("Invalid collision side for collision.");
            }
            return;
        }
    }
}