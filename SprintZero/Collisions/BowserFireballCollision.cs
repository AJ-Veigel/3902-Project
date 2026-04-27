using System;
using Microsoft.Xna.Framework;
using SprintZero.Marios;
 
namespace BowserFireballCollisions
{
    public static class BowserFireballCollision
    {
        public static void CheckBowserFireballMarioCollision(BowserFireball b, IMario mario, Action damageCallback)
        {
            if (!b.IsActive) return;

            if (b.BowserFireballCollider.Intersects(mario.MarioCollider))
            {
                damageCallback();
                b.Deactivate();
            }
        }
    }
}
 