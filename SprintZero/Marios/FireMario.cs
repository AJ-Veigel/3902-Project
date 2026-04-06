using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SoundManager;
using SprintZero.Marios;

public class FireMario : IMario
{
 public bool SlidingFlag { get; set; } = false;

    private const float SCALE = 4f;
    private const float MOVE_SPEED = 4f;
    private const float GRAVITY = 0.2f;
    private const float JUMP_POWER = -8f;
    private float groundY;
    private float currentPlatformY;

    public float jumpStartHeight { get; set; }
    public Vector2 velocity { get; set; }

    public float yVelocity { get; set; }
    public float xVelocity { get; set; }
    private MarioSprite marioSprites;

    public Vector2 location { get; set; }
    public Rectangle MarioCollider { get; set; }

    // State flags 
    public Boolean Jumping { get; set; }
    public Boolean Falling { get; set; }
    public Boolean Direction { get; set; } = true;
    public Boolean Sprinting { get; set; }
    public Boolean Crouching { get; set; }
    public Boolean Swimming { get; set; }
    public Boolean Moving { get; set; }
    public Boolean isOnGround { get; set; } = true;

    // Throw Timer
    private Boolean throwing;
    private double throwTimerMs;
    private const double THROW_DURATION_MS = 180;

    public FireMario(TextureAtlas fireMarioTexture, ContentManager content)
    {
        Moving = false;
        // Defaults
        location = new Vector2(300, 600);
        Direction = true;

        groundY = location.Y;
        currentPlatformY = groundY;

        yVelocity = 0f;
        xVelocity = 0f;

        marioSprites = new MarioSprite(fireMarioTexture, 2, location);

        // Set Mario Collider
        MarioCollider = marioSprites.UpdateCollider();

        isOnGround = false;
    
    }

    public FireMario(TextureAtlas fireMarioTexture, Vector2 pos,ContentManager content)
    {
        Moving = false;
        // Defaults
        location = pos;
        Direction = true;

        groundY = location.Y;
        currentPlatformY = groundY;

        yVelocity = 0f;
        xVelocity = 0f;

        marioSprites = new MarioSprite(fireMarioTexture, 2, location);

        // Set Mario Collider
        MarioCollider = marioSprites.UpdateCollider();

        isOnGround = false;
      
    }

    public void Move()
    {
        if (!Crouching || Jumping)
        {
            Moving = true;
            // If you’re throwing, you might want to ignore Move animation for a split second.
            // Up to you—this keeps movement but doesn’t override the throw pose.
            location = new Vector2(
                location.X + (Direction ? MOVE_SPEED : -MOVE_SPEED),
                location.Y
            );
            marioSprites.SetLocation(location);

            // Only set run animation if we’re not in a higher-priority pose
            if (!Jumping && !Swimming && !throwing && !Falling)
            {
                marioSprites.SetAnimatedSprite(Direction ? "moveRight" : "moveLeft");
            }
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
        Moving = false;
        if (!Jumping && !Crouching && !Swimming && !throwing)
        {
            marioSprites.SetSprite(Direction ? "standRight" : "standLeft");
        }
    }
    public void LandOnBlock(float blockTopY)
    {
        Vector2 newPos = location;
        newPos.Y = blockTopY - MarioCollider.Height;
        location = newPos;
        marioSprites.SetLocation(location);

        jumpStartHeight = location.Y;
        Jumping = false;
        Falling = false;
        isOnGround = true;

        marioSprites.SetSprite(Direction ? "standRight" : "standLeft");

        MarioCollider = marioSprites.UpdateCollider();
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
            yVelocity = JUMP_POWER;
            Jumping = true;
            Falling = false;
            jumpStartHeight = location.Y;
            isOnGround = false;

            // Update sprite
            marioSprites.SetSprite(Direction ? "jumpRight" : "jumpLeft");
            Music.jumpBigSound.Play();
        }
    }

    public void Bounce()
    {
        yVelocity = -6f;

        Jumping = true;
        Falling = false;
        isOnGround = false;
        jumpStartHeight = location.Y;

        marioSprites.SetSprite(Direction ? "jumpRight" : "jumpLeft");

        Music.jumpBigSound.Play();
    }

    public void Crouch()
    {
        if (!Falling && !Jumping && !Swimming)
        {
            if (Crouching)
            {
                location = new Vector2(location.X, location.Y + 10f * (SCALE));
                marioSprites.SetLocation(location);
                marioSprites.SetSprite(Direction ? "crouchRight" : "crouchLeft");
            }
            else if (!Crouching)
            {
                location = new Vector2(location.X, location.Y - 10f * (SCALE));
                marioSprites.SetLocation(location);
                marioSprites.SetSprite(Direction ? "crouchRight" : "crouchLeft");
            }
        }
    }

    public void Fireball()
    {
        throwing = true;
        throwTimerMs = 0;
        marioSprites.SetAnimatedSprite(Direction ? "throwRight" : "throwLeft");
        Music.fireballSound.Play();
    }

    public void Damage()
    {

    }
      public void GrabFlagPole()
    {
        Jumping = false;
        Falling = false;
        marioSprites.SetAnimatedSprite(Direction ? "flagpoleRight" : "flagpoleLeft");
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
                yVelocity += GRAVITY;
                // Move up
                newlocation.Y += yVelocity;

                // Check if reached peak
                if (yVelocity <= 0)
                {
                    Falling = true;
                }
            }
            else
            {
                if (yVelocity <= -JUMP_POWER)
                    yVelocity += GRAVITY;
                // Move down
                newlocation.Y += yVelocity;

                // Stop falling when reaching the ground
                if (isOnGround)
                {
                    yVelocity = 0f;
                    newlocation.Y = currentPlatformY;
                    Jumping = false;
                    Falling = false;

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