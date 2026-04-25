using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;

public class BowserFireball : IProjectile
{
    private readonly AnimatedSprite sprite;

    public Vector2 location { get; set; }
    public Rectangle BowserFireballCollider { get; private set; }
    public bool IsActive { get; private set; } = true;
    public bool Direction { get; }

    private Vector2 velocity;

    private const float X_SPEED = 4f;
    private const float SCALE = 4f;
    private const int SPRITE_WIDTH = 24;
    private const int SPRITE_HEIGHT = 8;

    private const int OFF_SCREEN_MARGIN = 128;

    public BowserFireball(AnimatedSprite fire, Vector2 startLocation, bool direction)
    {
        sprite = fire;
        location = startLocation;
        Direction = direction;

        velocity = new Vector2(Direction ? X_SPEED : -X_SPEED, 0f);

        BowserFireballCollider = BuildCollider();
    }

    public void Update(GameTime gameTime)
    {
        if (!IsActive) return;

        location += velocity;

        sprite.Update(gameTime);
        BowserFireballCollider = BuildCollider();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;

        sprite.Draw(spriteBatch, location);
    }

    public void CheckOffScreen(Rectangle cameraRect)
    {
        if (!IsActive) return;

        bool tooFarLeft = location.X + SPRITE_WIDTH * SCALE < cameraRect.Left - OFF_SCREEN_MARGIN;
        bool tooFarRight = location.X > cameraRect.Right + OFF_SCREEN_MARGIN;

        if (tooFarLeft || tooFarRight)
            IsActive = false;
    }
    public void Deactivate()
    {
        IsActive = false;
    }

    private Rectangle BuildCollider()
    {
        return new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(SPRITE_WIDTH * SCALE),
            (int)(SPRITE_HEIGHT * SCALE)
        );
    }
}