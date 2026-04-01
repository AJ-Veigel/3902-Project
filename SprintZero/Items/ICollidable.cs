using Microsoft.Xna.Framework;
using SprintZero.Sprites;

namespace SprintZero
{
    public interface ICollidable : ISprite
    {
        Hitbox Collider { get; set; }

        Rectangle RectCollider { get; set; }

        Hitbox.CollisionSide CollidesWith(ICollidable other)
        {
            return this.Collider.CollidesWith(this.location, other.Collider, other.location);
        }
        
    }
}
