using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Marios;
using System;

public class FlagMove : IBlock
{
    private AnimatedSprite flagSprite;
    private const float SCALE = 4f;
    private const float marioSlideSpeed = 2f;

    private bool marioSliding = false;
    private IMario slidingMario;

    public Vector2 location { get; set; }
    public Rectangle Collider { get;  set; }
    private float bottomY; 

    public FlagMove(AnimatedSprite sprite)
    {
        flagSprite = sprite;
        flagSprite.Scale = new Vector2(SCALE);
        flagSprite.Pause();

        location = new Vector2(500, 50); // starting position
        bottomY = 300f;

        UpdateCollider();
    }

    public Boolean GetCollidable()
    {
        return true;
    }

    public void Update(GameTime gameTime)
    {
        Vector2 zero = new Vector2(0, 0);
        Rectangle marioRect = slidingMario.Collider.getBoundingRectangle(zero);
        flagSprite.Update(gameTime);

        // Slide Mario down the flag
        if (marioSliding && slidingMario != null)
        {
            Vector2 newMarioPos = slidingMario.location;
            newMarioPos.Y = Math.Min(newMarioPos.Y + marioSlideSpeed, bottomY - marioRect.Height);
            slidingMario.location = newMarioPos;

            // Update Mario collider
            marioRect = new Rectangle(
                (int)slidingMario.location.X,
                (int)slidingMario.location.Y,
                marioRect.Width,
                marioRect.Height
            );

            // Stop sliding when Mario reaches the bottom
            if (slidingMario.location.Y >= bottomY - marioRect.Height)
            {
                slidingMario.EndFlagPole();
                marioSliding = false;
                slidingMario = null;
            }
        }

        UpdateCollider();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        flagSprite.Draw(spriteBatch, location);
    }

    private void UpdateCollider()
    {
        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(flagSprite.Width * SCALE),
            (int)(flagSprite.Height * SCALE)
        );
    }

    public void onCollision(IMario mario, CollisionSide theSide)
    {
        Vector2 zero = new Vector2(0, 0);
        Rectangle marioRect = mario.Collider.getBoundingRectangle(zero);
        Rectangle slidingMarioRect = mario.Collider.getBoundingRectangle(zero);
        // Start sliding if Mario hits the flag
        if (!marioSliding && marioRect.Intersects(Collider))
        {
            marioSliding = true;
            slidingMario = mario;

            // Start flag animation
            flagSprite.Play();

            // Tell Mario to play the flagpole animation
            slidingMario.GrabFlagPole();

            // Attach Mario to flag horizontally
            slidingMario.location = new Vector2(
                location.X - slidingMarioRect.Width,
                slidingMario.location.Y
            );
        }
    }
}