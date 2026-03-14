using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.blocks;
using SprintZero.Marios;

public class MediumTube : IBlock
{
    private const float SCALE = 4f;
    private TextureRegion sprite;
    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }
    public MediumTube(TextureRegion region)
    {
        sprite = region;

        // Default location
        location = new Vector2(800, 650);

        // Initialize collider
        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(sprite.Width * SCALE),
            (int)(sprite.Height * SCALE)
        );
    }

    public Boolean GetCollidable()
    {
        return true;
    }
    public void Update(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch, location, Color.White, 0f, Vector2.One, 4f, SpriteEffects.None, 0f);

    }

    public void onCollision(IMario mario, CollisionSide theSide)
    {

        if (theSide == CollisionSide.Top)
        {
            mario.location = new Vector2(mario.location.X, location.Y - mario.MarioCollider.Height);
            mario.Jumping = false;
            mario.Falling = false;
            mario.isOnGround = true;
        }
        else if (theSide == CollisionSide.Left)
        {
            mario.location = new Vector2(Collider.Left - mario.MarioCollider.Width, mario.location.Y);
        }
        else if (theSide == CollisionSide.Right)
        {
            mario.location = new Vector2(Collider.Right, mario.location.Y);
        }
    }
}