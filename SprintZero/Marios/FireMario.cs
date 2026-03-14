using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Marios;

public class FireMario : IMario
{
    private const float SCALE = 4f;
    private const float MOVE_SPEED = 4f;
    private const float GRAVITY = 4f;
    private const float JUMP_VELOCITY = 4f;
    private float currentPlatformY;

    public float jumpStartHeight { get; set; }
    public Vector2 velocity { get; set; }

    public float yVelocity { get; set; }
    public float xVelocity { get; set; }
    private MarioSprite marioSprites;

    // Currently drawing 
    private TextureRegion currentSprite;
    private AnimatedSprite currentASprite;

    public Vector2 location { get; set; }
    public Rectangle MarioCollider { get; set; }

    // State flags 
    public bool Jumping { get; set; }
    public bool Falling { get; set; }
    public bool Direction { get; set; } = true;
    public bool Sprinting { get; set; }
    public bool Crouching { get; set; }
    public bool Swimming { get; set; }
    public bool isOnGround { get; set; } = true;

    // Throw Timer
    private bool throwing;
    private double throwTimerMs;
    private const double THROW_DURATION_MS = 180;

    public FireMario(TextureAtlas fireMarioTexture)
    {
        // Defaults
        location = new Vector2(300, 600);
        Direction = true;

        marioSprites = new MarioSprite(fireMarioTexture, 2, location);

        // Set Mario Collider
        MarioCollider = marioSprites.UpdateCollider();
    }

    public FireMario(TextureAtlas fireMarioTexture, Vector2 pos)
    {
        // Defaults
        location = pos;
        Direction = true;

        marioSprites = new MarioSprite(fireMarioTexture, 2, location);

        // Set Mario Collider
        MarioCollider = marioSprites.UpdateCollider();
    }

    private static void ApplyScale(AnimatedSprite sprite)
    {
        if (sprite != null)
        {
            sprite.Scale = new Vector2(SCALE);
        }
    }

    private void SetRegion(TextureRegion region)
    {
        currentSprite = region;
        currentASprite = null;
    }

    private void SetAnimated(AnimatedSprite anim)
    {
        currentASprite = anim;
        currentSprite = null;
    }

    public void Move()
    {
        // If you’re throwing, you might want to ignore Move animation for a split second.
        // Up to you—this keeps movement but doesn’t override the throw pose.
        location = new Vector2(
            location.X + (Direction ? MOVE_SPEED : -MOVE_SPEED),
            location.Y
        );
        marioSprites.SetLocation(location);

        // Only set run animation if we’re not in a higher-priority pose
        if (!Jumping && !Crouching && !Swimming && !throwing && !Falling)
        {
            marioSprites.SetAnimatedSprite(Direction ? "moveRight" : "moveLeft");
        }
    }

    public void setAppropriate()
    {
        if (Swimming)
            marioSprites.SetSprite(Direction ? "swimRight" : "swimLeft");
        else if (Crouching)
            marioSprites.SetSprite(Direction ? "crouchRight" : "crouchLeft");
        else if (Jumping)
            marioSprites.SetSprite(Direction ? "jumpRight" : "jumpLeft");
        else
            marioSprites.SetSprite(Direction ? "standRight" : "standLeft");
    }

    public void StopMove()
    {
        if (!Jumping && !Crouching && !Swimming && !throwing)
        {
            marioSprites.SetSprite(Direction ? "standRight" : "standLeft");
        }
    }
    public void LandOnBlock(float blockTopY)
    {
        Vector2 newPos = location;
        newPos.Y = blockTopY - currentSprite.Height * SCALE;
        location = newPos;
        marioSprites.SetLocation(location);

        jumpStartHeight = location.Y;
        Jumping = false;
        Falling = false;
        isOnGround = true;

        marioSprites.SetSprite(Direction ? "standRight" : "standLeft");

        MarioCollider = new Rectangle(
            (int)location.X,
            (int)location.Y,
            currentSprite.Width * (int)SCALE,

            currentSprite.Height * (int)SCALE
        );
        marioSprites.UpdateCollider();
    }
    public void UpdateAirSpriteForDirection()
    {
        if (throwing) return;
        if (Jumping || Falling)
        {
            marioSprites.SetSprite(Direction ? "jumpRight" : "jumpLeft");
        }
    }

    public void Jump()
    {
        if (isOnGround)
        {
            Jumping = true;
            Falling = false;
            jumpStartHeight = location.Y;
            isOnGround = false;

            // Update sprite
            marioSprites.SetSprite(Direction ? "jumpRight" : "jumpLeft");
        }
    }
    // if(Jumping || Falling)
    //     return;
    // jumpStartHeight = location.Y;
    // Jumping = true;
    // SetRegion(Direction ? jumpingRightSprite : jumpingLeftSprite);

    public void GrabFlagPole()
    {
        Jumping = false;
        Falling = false;
        marioSprites.SetAnimatedSprite(Direction ? "flagpoleRight" : "flagpoleLeft");
    }
    public void Crouch()
    {

    }

    public void Fireball()
    {
        throwing = true;
        throwTimerMs = 0;
        marioSprites.SetAnimatedSprite(Direction ? "throwRight" : "throwLeft");
    }

    public void Damage()
    {

    }
    public void EndFlagPole()
    {
        marioSprites.SetSprite(Direction ? "flagpoleRight" : "flagpoleLeft");

        isOnGround = true;
        Jumping = false;
        Falling = false;
    }
    public Vector2 FireballSpawnlocation
    {
        get
        {
            float offsetX = Direction ? 40f : -10f;
            float offsetY = 40f;
            return new Vector2(location.X + offsetX, location.Y + offsetY);
        }
    }

    public void Update(GameTime gameTime)
    {
        Vector2 newlocation = location;

        // Handle jumping and falling
        if (Jumping || Falling)
        {
            if (!Falling)
            {
                // Move up
                newlocation.Y -= JUMP_VELOCITY;

                // Check if reached peak
                if (newlocation.Y <= jumpStartHeight - 100)
                {
                    Falling = true;
                }
            }
            else
            {
                // Move down
                newlocation.Y += GRAVITY;

                // Stop falling when reaching the ground
                if (newlocation.Y >= jumpStartHeight)
                {
                    newlocation.Y = jumpStartHeight;
                    Jumping = false;
                    Falling = false;
                    isOnGround = true;

                    marioSprites.SetSprite(Direction ? "standRight" : "standLeft");
                }
            }
            UpdateAirSpriteForDirection();
        }

        location = newlocation;
        marioSprites.SetLocation(location);

        // Update throwing timer
        if (throwing)
        {
            throwTimerMs += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (throwTimerMs >= THROW_DURATION_MS)
            {
                throwing = false;
                throwTimerMs = 0;
                setAppropriate();
            }
        }

        MarioCollider = marioSprites.UpdateCollider();

        if ((Jumping || Falling) && !isOnGround)
        {
            if (Direction)
                marioSprites.SetSprite("jumpRight");
            else
                marioSprites.SetSprite("jumpLeft");
        }

        marioSprites.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        marioSprites.Draw(spriteBatch);
    }
}