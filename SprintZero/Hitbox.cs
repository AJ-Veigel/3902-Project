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
            // Does not actually collide right now!
            return CollisionSide.None;
        }

    }
}
