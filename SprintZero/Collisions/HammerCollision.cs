using System;
using Microsoft.Xna.Framework;
using SprintZero.Marios;
 
namespace HammerCollisions
{
    public static class HammerCollision
    {
        // The hammer itself is NOT deactivated here — Bowser's hammers persist until
        // they scroll off-screen 
        public static void CheckHammerMarioCollision(Hammer hammer, IMario mario, Action damageCallback)
        {
            if (!hammer.IsActive) return;

            if (mario.Invincible) return;

            if (hammer.HammerCollider.Intersects(mario.MarioCollider))
            {
                damageCallback();
            }
        }
    }
}
 