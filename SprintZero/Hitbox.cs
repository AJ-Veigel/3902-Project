using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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


        // Returns the side of the second hitbox that the first hitbox collides with, or CollisionSide.None if no collision.
        public CollisionSide CollidesWith(Vector2 selfOffset, Hitbox other, Vector2 otherOffset)
        {
            Vector2 diff = otherOffset - (selfOffset + new Vector2(this.x, this.y)); // How much offset from this hitbox the other hitbox is.
            // This X,Y is 0.
            var otherX = other.x + diff.X; var otherY = other.y + diff.Y;
            if (    otherX > this.width
                ||  otherY > this.height
                || (otherX + other.width) < 0.0f
                || (otherY + other.height) < 0.0f
            ) { return CollisionSide.None;} // 

            var left = this.width - otherX; // How 'far' this collides with the left side of other
            var top = this.height - otherY; // How 'far' this collides with the top side of other u get the point.
            var right = otherX + other.width;
            var bottom = otherY + other.height;

            var max = (new float[] { left, top, right, bottom }).Max(); // Evil?

            if (max == left) { return CollisionSide.Left; }
            else if (max == top) { return CollisionSide.Top; }
            else if (max == right) { return CollisionSide.Right; }
            return CollisionSide.Bottom;
        }

    }
}
