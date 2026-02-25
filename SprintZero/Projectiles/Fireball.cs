using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class Fireball : IProjectile
{
    private enum FireballState { Rolling, Popped }

    private FireballState state = FireballState.Rolling;

    private readonly AnimatedSprite rolling;
    private readonly AnimatedSprite popped;

    public Vector2 location { get; set; }
    private Vector2 velocity;

    private const float X_SPEED = 6f;
    private const float GRAVITY = 0.5f;
    private const float BOUNCE_VELOCITY = -6f;

    private double popTimerMs = 0;
    private const double POP_DURATION_MS = 200;

    public bool IsActive { get; private set; } = true;
    public bool Direction { get; }
    public Fireball(AnimatedSprite fireballRolling, AnimatedSprite fireballPop, Vector2 location, bool Direction)
    {
        rolling = fireballRolling;
        popped = fireballPop;
        this.location = location;
        this.Direction = Direction;
        velocity = new Vector2(Direction ? X_SPEED : -X_SPEED, 0f);
    }



    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;
        if (state == FireballState.Rolling)
        {
            rolling.Draw(spriteBatch, location);
        }
        else
        {
            popped.Draw(spriteBatch, location);
        }
    }

    public void Pop()
    {
        if (state == FireballState.Popped) return;

        state = FireballState.Popped;
        popTimerMs = 0;

        // freeze movement so it doesn't slide while popping
        velocity = Vector2.Zero;
    }



    public void Update(GameTime gameTime)
    {
        if (!IsActive) return;

        double dtMs = gameTime.ElapsedGameTime.TotalMilliseconds;

        if (state == FireballState.Rolling)
        {
            // physics
            velocity.Y += GRAVITY;
            location += velocity;

            // update rolling animation
            rolling.Update(gameTime);

            // example ground bounce (replace with real collision later)
            float groundY = 750f;             // ground
            float fireballHeight = 16f * 4f;  // base height * scale 
            if (location.Y + fireballHeight >= groundY)
            {
                location = new Vector2(location.X, groundY - fireballHeight);
                velocity.Y = BOUNCE_VELOCITY;
            }
        }
        else // Popped
        {
            popTimerMs += dtMs;
            popped.Update(gameTime);

            if (popTimerMs >= POP_DURATION_MS)
                IsActive = false; // remove from manager list
        }
        if (location.X >= 1888 || location.X <= 0)
            Pop();
    }
}
