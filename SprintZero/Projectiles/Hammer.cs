using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SpriteZero.Sprites;
using System;
 
public class Hammer : IProjectile
{
    private readonly AnimatedSprite sprite;
    public Vector2 location { get; set; }
    public bool IsActive { get; private set; } = true;
    public bool Direction { get; }           //  true : right | false : left 
    public Rectangle HammerCollider { get; private set; }
    private const float X_SPEED = 3.5f;
    private const float INITIAL_Y_VELOCITY = -9f;
 
    // Gravitational acceleration applied each frame.
    private const float GRAVITY = 0.35f;
    private Vector2 velocity;
    private const float SCALE = 4f;
    private const int SPRITE_SIZE = 16;
 
    // Hammers only deactivate once completely outside the visible area (+ margin).
    private const int OFF_SCREEN_MARGIN = 128;
 

    public Hammer(AnimatedSprite hammerSprite, Vector2 startLocation, bool direction)
    {
        sprite = hammerSprite;
        location = startLocation;
 
        //  true : right | false : left 
        this.Direction = direction;
        velocity = new Vector2(Direction ? X_SPEED : -X_SPEED, INITIAL_Y_VELOCITY);
 
        HammerCollider = BuildCollider();
    }
 
    public void Update(GameTime gameTime)
    {
        if (!IsActive) return;
 
        // Apply gravity and move.
        velocity = new Vector2(velocity.X, velocity.Y + GRAVITY);
        location += velocity;
 
        sprite.Update(gameTime);
        HammerCollider = BuildCollider();
    }
 
    // Deactivates the hammer when it has scrolled fully outside the camera viewport.
    // Call this every frame after Update(), passing the current camera rectangle.
    // Hammers are intentionally NOT deactivated before leaving the screen so that
    // a hammer thrown upward off the top of the viewport can still come back down.
    public void CheckOffScreen(Rectangle cameraRect)
    {
        if (!IsActive) return;
 
        bool tooFarLeft = location.X + SPRITE_SIZE * SCALE < cameraRect.Left  - OFF_SCREEN_MARGIN;
        bool tooFarRight = location.X > cameraRect.Right  + OFF_SCREEN_MARGIN;
        bool tooFarDown = location.Y > cameraRect.Bottom + OFF_SCREEN_MARGIN;
 
        if (tooFarLeft || tooFarRight || tooFarDown)
            IsActive = false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;
        sprite.Draw(spriteBatch, location);
    }

    private Rectangle BuildCollider() =>
        new Rectangle(
            (int)location.X,
            (int)location.Y,
            (int)(SPRITE_SIZE * SCALE),
            (int)(SPRITE_SIZE * SCALE));
}