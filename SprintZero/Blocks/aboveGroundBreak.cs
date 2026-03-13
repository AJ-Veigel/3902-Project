using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
<<<<<<< HEAD
using SprintZero.blocks;
using SprintZero.Marios;
=======
using SpriteZero.Marios;
using SpriteZero.blocks;
>>>>>>> origin/block-mario-fix

public class AboveGroundBreak : IBlock
{
    private AnimatedSprite sprite;
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }
    private const float SCALE = 4f;
    private Vector2 velocity;
    private float gravity = 0.5f;
    private bool isBroken = false;
    private bool playBreakingAnimation = false;

    public AboveGroundBreak(AnimatedSprite animated)
    {
        sprite = animated;
        sprite.Scale = new Vector2(SCALE);
        sprite.Pause();
        location = new Vector2(300, 500);
        velocity = Vector2.Zero;
        Collider = new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height);
    }

    public Boolean GetCollidable()
    {
        return true;
    }


    public void Update(GameTime gameTime)
    {
        if (playBreakingAnimation)
        {
            sprite.Play();
            sprite.Update(gameTime);

            velocity.Y += gravity;
            location += velocity;
        }
        else
        {
            sprite.Update(gameTime);
        }

        Collider = !isBroken
            ? new Rectangle((int)location.X, (int)location.Y, (int)sprite.Width, (int)sprite.Height)
            : Rectangle.Empty;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!isBroken)
            sprite.Draw(spriteBatch, location);
    }
public void onCollision(IMario mario, CollisionSide side)
{
    if (theSide == CollisionSide.Bottom && !isBroken)
    {
        if (mario is BigMario || mario is FireMario)
        {
            // Break the block
            isBroken = true;
            playTheBreakingAnimation = true;
            velocity = new Vector2(-6f, -8f);
        }
    }

    if (shouldBreak)
    {
        isBroken = true;
        playBreakingAnimation = true;
        velocity = new Vector2(-6f, -8f);
        mario.Jumping = true;
        mario.Falling = true;
    }
}
}
