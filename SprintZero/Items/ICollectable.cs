using Microsoft.Xna.Framework;
using SprintZero.Sprites;

namespace SprintZero.Items
{
    public interface ICollectable : ISprite
    {
        Hitbox Collider { get; set; }

        Rectangle RectCollider { get; set; }
        bool Collected {get;set;} 

        Hitbox.CollisionSide CollidesWith(ICollectable other)
        {
            return Collider.CollidesWith(location, other.Collider, other.location);
        }
        
    }
}
