using Microsoft.Xna.Framework;
using SprintZero.Sprites;

namespace SprintZero
{
    public interface ICollectable : ISprite
    {
        Hitbox Collider { get; set; }

        Rectangle RectCollider { get; set; }

        Hitbox.CollisionSide CollidesWith(ICollectable other)
        {
            return this.Collider.CollidesWith(this.location, other.Collider, other.location);
        }
        
    }
}
