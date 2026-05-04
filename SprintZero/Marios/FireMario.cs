using System;
using System.Diagnostics.CodeAnalysis;
using MarioMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SoundManager;
using SprintZero;
using SprintZero.Marios;

public class FireMario : IMario
{
    public bool SlidingFlag { get; set; } = false;

    private const float SCALE = 4f;
    private const float MOVE_SPEED = 4f;
    private const float GRAVITY = 0.2f;
    private const float JUMP_POWER = -11;
    private float groundY;
    private Game1 game;
    public float currentPlatformY { get; set; }

    public float jumpStartHeight { get; set; }
    public Vector2 velocity { get; set; }

    public float yVelocity { get; set; }
    public float xVelocity { get; set; }
    private MarioSprite marioSprites;

    public Vector2 location { get; set; }
    public Rectangle MarioCollider { get; set; }

    // State flags 
    public bool Jumping { get; set; }
    public bool Falling { get; set; }
    public bool Direction { get; set; } = true;
    public bool Sprinting { get; set; }
    public bool Crouching { get; set; }
    public bool Swimming { get; set; }
    public bool Moving { get; set; }
    public bool isOnGround { get; set; } = true;
    public bool Invincible { get; set; } = true;
    private float invincibilityTimer = 0f;

    // Throw Timer
    public bool throwing { get; set; } = false;
    private double throwTimerMs;
    private const double THROW_DURATION_MS = 180;
    public bool AutoWalking { get; set; } = false;
    public bool WinState { get; set; } = false;
    public bool IsStarPower { get; set; } = false;
    private bool invincibleTint = false;



    public FireMario(TextureAtlas fireMarioTexture, ContentManager content, Game1 game)
    {
        Moving = false;
        // Defaults
        location = new Vector2(300, 600);
        Direction = true;
        this.game = game;

        groundY = location.Y;
        currentPlatformY = groundY;

        yVelocity = 0f;
        xVelocity = 0f;

        marioSprites = new MarioSprite(fireMarioTexture, 2, location);

        // Set Mario Collider
        MarioCollider = marioSprites.UpdateCollider();

        isOnGround = false;

    }

    public FireMario(TextureAtlas fireMarioTexture, Vector2 pos, ContentManager content)
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
        MarioMovementManager.Move(this, marioSprites);
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
        MarioMovementManager.StopMove(this, marioSprites);
    }
    public void LandOnBlock(float blockTopY)
    {
        MarioMovementManager.LandOnBlock(this, marioSprites, blockTopY);
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
        MarioMovementManager.Jump(this, marioSprites);
    }

    public void Bounce()
    {
        MarioMovementManager.Bounce(this, marioSprites);
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
        marioSprites.UpdateCollider();
        Music.fireballSound.Play();
    }

    public void Damage()
    {
        game.Damage();
    }
    public void GrabFlagPole()
    {
        MarioMovementManager.GrabFlagPole(this, marioSprites);
    }
    public void EndFlagPole()
    {
        MarioMovementManager.EndFlagPole(this, marioSprites);
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
    public void BecomeInvincible()
    {
        Invincible = true;
        IsStarPower = true;
        invincibilityTimer = -10f;
    }
    public void SetLocation(Vector2 pos)
    {
        location = pos;
        marioSprites.SetLocation(pos);
        Console.WriteLine(marioSprites.location);
    }

    public Vector2 GetLocation()
    {
        return location;
    }

    public void Update(GameTime gameTime)
    {
        invincibilityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (invincibilityTimer > 1)
        {
            Invincible = false;

            if (IsStarPower)
            {
                IsStarPower = false;
                Music.PlayBackground();
            }
        }
        if (SlidingFlag)
        {
            float slideSpeed = 2.5f;

            Vector2 nextPosition = new Vector2(location.X, location.Y + slideSpeed);
            if (nextPosition.Y >= currentPlatformY)
            {
                nextPosition.Y = currentPlatformY;
                location = nextPosition;
                marioSprites.SetLocation(location);

                EndFlagPole();
            }
            else
            {
                location = nextPosition;
                marioSprites.SetLocation(location);
            }

            MarioCollider = marioSprites.UpdateCollider();
            return;
        }

        invincibilityTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (invincibilityTimer > 1)
        {
            Invincible = false;
            if (IsStarPower)
            {
                IsStarPower = false;
                Music.PlayBackground();
            }
        }
        if(IsStarPower)
        {
            if(-(int)(invincibilityTimer * 6) % 2 == 0)
            {
                invincibleTint = true;
            }
            else
            {
                invincibleTint = false;
            }
        }
        else
        {
            invincibleTint = false;
        }
        
        if (AutoWalking)
        {
            float castleX = 100f;

            xVelocity = 2f;
            location = new Vector2(location.X + xVelocity, location.Y);

            marioSprites.SetAnimatedSprite("moveRight");
            marioSprites.SetLocation(location);

            if (location.X >= castleX)
            {
                AutoWalking = false;
                xVelocity = 0;
                marioSprites.SetSprite("standRight");
            }

            MarioCollider = marioSprites.UpdateCollider();
            return;
        }

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

        if (isOnGround)
        {
            if (!Moving) StopMove();
            Falling = false;
            yVelocity = 0f;
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

        if ((Jumping || Falling) && !isOnGround)
        {
            if (Direction)
                marioSprites.SetSprite("jumpRight");
            else
                marioSprites.SetSprite("jumpLeft");
        }

        MarioCollider = marioSprites.UpdateCollider();

        marioSprites.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        marioSprites.Draw(spriteBatch, invincibleTint);
    }
}