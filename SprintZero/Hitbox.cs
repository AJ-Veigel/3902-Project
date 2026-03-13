using System;
using Microsoft.Xna.Framework;

namespace SprintZero
{
    public class Hitbox
    {
        public readonly float x, y;
        public readonly float width, height;

        public enum CollisionSide { None, Left, Right, Top, Bottom }

        public Hitbox(float x, float y, float width, float height)
        {
            this.x = x; this.y = y; this.width = width; this.height = height;
        }

        private static float MinimumPositive(params float[] vals)
        {
            float min = float.PositiveInfinity;
            for (int i = 0; i < vals.Length; i++)
            {
                if (vals[i] < min && vals[i] >= 0.0f)
                {
                    min = vals[i];
                }
            }
            return min;
        }

        // Returns the side of the second hitbox that the first hitbox collides with, or CollisionSide.None if no collision.
        public CollisionSide CollidesWith(Vector2 selfOffset, Hitbox other, Vector2 otherOffset)
        {
            Vector2 diff = otherOffset - (selfOffset + new Vector2(this.x, this.y)); // How much offset from this hitbox the other hitbox is.
            // This X,Y is 0.
            float otherX = other.x + diff.X; float otherY = other.y + diff.Y;
            if (    otherX > this.width
                ||  otherY > this.height
                || (otherX + other.width) < 0.0f
                || (otherY + other.height) < 0.0f
            ) { return CollisionSide.None;}

            float left = this.width - otherX; // How 'far' this collides with the left side of other
            float top = this.height - otherY; // How 'far' this collides with the top side of other u get the point.
            float right = otherX + other.width;
            float bottom = otherY + other.height;

            float real = MinimumPositive(left, top, right, bottom);

            if (real == left) { return CollisionSide.Left; }
            else if (real == top) { return CollisionSide.Top; }
            else if (real == right) { return CollisionSide.Right; }
            return CollisionSide.Bottom;
        }

        public Rectangle getBoundingRectangle(Vector2 offset)
        {
            float thisX = this.x - offset.X;
            float thisY = this.y - offset.Y;
            return new Rectangle(
                (int)MathF.Floor(thisX), 
                (int)MathF.Floor(thisY), 
                (int)MathF.Ceiling(this.width),
                (int)MathF.Ceiling(this.height)
            );
        }
    }
}
