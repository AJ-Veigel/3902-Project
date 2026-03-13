using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.blocks;
using SpriteZero.Marios;

public class MediumTube : IBlock
{
    private TextureRegion sprite;

    public Vector2 location { get; set; }
    public Rectangle Collider { get; set; }

    private const float SCALE = 4f;

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

    public void Update(GameTime gameTime)
    {
        // Keep collider in sync with location
        Collider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(sprite.Width * SCALE),
            (int)(sprite.Height * SCALE)
        );
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(
            spriteBatch,
            location,
            Color.White,
            0f,
            Vector2.One,
            SCALE,
            SpriteEffects.None,
            0f
        );
    }

    public void onCollision(IMario mario, CollisionSide theSide)
    {
        // Make a local copy to avoid "cannot modify return value"
        Vector2 newPos = mario.position;

        if (theSide == CollisionSide.Top)
        {
            // Land Mario on top
            newPos.Y = Collider.Top - mario.MarioCollider.Height;
            mario.Jumping = false;
            mario.Falling = false;
            mario.isOnGround = true;
        }
        else if (theSide == CollisionSide.Left)
        {
            newPos.X = Collider.Left - mario.MarioCollider.Width;
        }
        else if (theSide == CollisionSide.Right)
        {
            newPos.X = Collider.Right;
        }
        else if (theSide == CollisionSide.Bottom)
        {
            newPos.Y = Collider.Bottom;
            mario.Falling = true; // Gravity takes over
        }

        // Assign the modified position back
        mario.position = newPos;
    }
}