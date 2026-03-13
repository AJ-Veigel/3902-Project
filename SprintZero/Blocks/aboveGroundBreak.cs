using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.blocks;
using SpriteZero.Marios;

public class AboveGroundBreak : IBlock
{
    private AnimatedSprite sprite;
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }
    private const float SCALE = 4f;
    private Vector2 velocity;
    private float gravity = 0.5f;
    private bool isBroken = false;
    private bool playTheBreakingAnimation = false;

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
        if (playTheBreakingAnimation)
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


      if (!isBroken)
{
    Collider = new Rectangle(
        (int)location.X,
        (int)location.Y,
        (int)sprite.Width,
        (int)sprite.Height
    );
}
else
{
    Collider = Rectangle.Empty;
}
    }

public void Draw(SpriteBatch spriteBatch)
{
    if (!isBroken)
    {
        sprite.Draw(spriteBatch, location);
    }
}

   public void onCollision(IMario mario, CollisionSide theSide)
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

    else if (theSide == CollisionSide.Top && !isBroken)
    {
        mario.location = new Vector2(mario.location.X, location.Y - mario.MarioCollider.Height);
        mario.Falling = true;
        mario.Jumping = false;
    }
}
}