using System;
using SpriteZero.Sprites;

namespace SprintZero
{
    public interface ICollidable : ISprite
    {
        Hitbox Collider { get; set; }

        Hitbox.CollisionSide CollidesWith(ICollidable other)
        {
            return this.Collider.CollidesWith(this.location, other.Collider, other.location);
        }
    }
}
