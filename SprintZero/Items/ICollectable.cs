using Microsoft.Xna.Framework;
using SprintZero.Sprites;

namespace SprintZero.Items
{
    public interface ICollectable : ISprite
    {
        Rectangle RectCollider { get; set; }
        float VelocityX { get; set; }
        float VelocityY { get; set; }
        bool Collected { get; set; }
        bool onGround { get; set; }
        void ReverseDirection();
    }
}
