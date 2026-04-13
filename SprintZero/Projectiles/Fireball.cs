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

    private int bounceCount = 0;
    private const int MAX_BOUNCE = 8;

    public bool IsActive { get; private set; } = true;
    public bool Direction { get; }
    public Rectangle FireballCollider { get; set; }
    private const float SCALE = 4f;
    public Fireball(AnimatedSprite fireballRolling, AnimatedSprite fireballPop, Vector2 location, bool Direction)
    {
        rolling = fireballRolling;
        popped = fireballPop;
        this.location = location;
        this.Direction = Direction;
        velocity = new Vector2(Direction ? X_SPEED : -X_SPEED, 0f);
        FireballCollider = new Rectangle((int)location.X, (int)location.Y, 8 * (int)SCALE, 8 * (int)SCALE); 
    }


    public void Bounce(float blockTop)
    {
        location = new Vector2(location.X, blockTop - 16f * SCALE); // snap above block
        velocity.Y = BOUNCE_VELOCITY;
        bounceCount++;
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

            float popY = 1000.0f;

            if (location.Y > popY)
            {
                Pop();
            }
        }
        else // Popped
        {
            popTimerMs += dtMs;
            popped.Update(gameTime);

            if (popTimerMs >= POP_DURATION_MS)
                IsActive = false; // remove from manager list
        }
        if (bounceCount > MAX_BOUNCE) { Pop(); }
        FireballCollider = new Rectangle((int)location.X, (int)location.Y, 8 * (int)SCALE, 8 * (int)SCALE); 
    }
}
