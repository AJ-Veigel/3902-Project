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
    public Rectangle Collider { get; set; }
    private float bottomY;

    public FlagMove(AnimatedSprite sprite)
    {
        flagSprite = sprite;
        flagSprite.Scale = new Vector2(SCALE);
        flagSprite.Pause();

        location = new Vector2(500, 50); // starting location
        bottomY = 300f;

        UpdateCollider();
    }

    public void Update(GameTime gameTime)
    {
        flagSprite.Update(gameTime);

        // Slide Mario down the flag
        if (marioSliding && slidingMario != null)
        {
            Vector2 newMarioPos = slidingMario.location;
            newMarioPos.Y = Math.Min(newMarioPos.Y + marioSlideSpeed, bottomY - slidingMario.MarioCollider.Height);
            slidingMario.location = newMarioPos;

            // Update Mario collider
            slidingMario.MarioCollider = new Rectangle(
                (int)slidingMario.location.X,
                (int)slidingMario.location.Y,
                slidingMario.MarioCollider.Width,
                slidingMario.MarioCollider.Height
            );

            // Stop sliding when Mario reaches the bottom
            if (slidingMario.location.Y >= bottomY - slidingMario.MarioCollider.Height)
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
      
        if (!marioSliding && mario.MarioCollider.Intersects(Collider))
        {
            marioSliding = true;
            slidingMario = mario;

            flagSprite.Play();

            slidingMario.GrabFlagPole();

            slidingMario.location = new Vector2(
                location.X - slidingMario.MarioCollider.Width,
                slidingMario.location.Y
            );
        }
    }
}