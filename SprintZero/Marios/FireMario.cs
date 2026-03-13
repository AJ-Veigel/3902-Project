using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero;
using SprintZero.Marios;

public class FireMario : IMario
{
    private const float SCALE = 4f;
    private const float MOVE_SPEED = 4f;
    private const float GRAVITY = 4f;
    private const float JUMP_VELOCITY = 4f;

    public float jumpStartHeight { get; set; }
    public Vector2 velocity { get; set; }

    public float yVelocity { get; set; }
    public float xVelocity { get; set; }
    private MarioSprite marioSprites;

    public Vector2 location { get; set; }
    public Hitbox Collider {get; set; }
    public Boolean Collidable { get; set; }
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

        jumpStartHeight = location.Y;

        marioSprites = new MarioSprite(fireMarioTexture, 2, location);

        isOnGround = true;

        Collider = marioSprites.UpdateCollider();
    }

    public FireMario(TextureAtlas fireMarioTexture, Vector2 pos)
    {
        location = pos;

        jumpStartHeight = location.Y;

        marioSprites = new MarioSprite(fireMarioTexture, 2, location);

        isOnGround = true;

        Collider = marioSprites.UpdateCollider();
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
            if (Direction)
            {
                marioSprites.SetAnimatedSprite("moveRight");
            }
            else
            {
                marioSprites.SetAnimatedSprite("moveLeft");
            }
        }
    }

    public void setAppropriate()
    {
        if (Swimming)
        {
            if(Direction)
            {
                marioSprites.SetAnimatedSprite("swimRight");
            }
            else
            {
                marioSprites.SetAnimatedSprite("swimLeft");
            }
        }
        else if (Crouching)
        {
            if(Direction)
            {
                marioSprites.SetSprite("crouchRight");
            }
            else
            {
                marioSprites.SetSprite("crouchLeft");
            }
        }
        else if (Jumping)
        {
            if(Direction)
            {
                marioSprites.SetSprite("jumpRight");
            }
            else
            {
                marioSprites.SetSprite("jumpLeft");
            }
        }
        else
        {
            if(Direction)
            {
                marioSprites.SetSprite("standRight");
            }
            else
            {
                marioSprites.SetSprite("standLeft");
            }
        }
    }

    public void StopMove()
    {
        if (!Jumping && !Crouching && !Swimming && !throwing)
        {
            if(Direction)
            {
                marioSprites.SetSprite("standRight");
            }
            else
            {
                marioSprites.SetSprite("standLeft");
            }
        }
    }
    public void LandOnBlock(float blockTopY)
    {
        // Copy location to a local variable
        Vector2 newPos = location;

        // Modify the Y value
        newPos.Y = blockTopY - marioSprites.GetSprite().Height * SCALE;

        // Assign back
        location = newPos;

        jumpStartHeight = location.Y;
        Jumping = false;
        Falling = false;
        isOnGround = true;

        if(Direction)
        {
            marioSprites.SetSprite("standRight");
        }
        else
        {
            marioSprites.SetSprite("standLeft");
        }

        Collider = marioSprites.UpdateCollider();
    }
    public void UpdateAirSpriteForDirection()
    {
        if (throwing) return;
        if (Jumping || Falling)
        {
            if(Direction)
            {
                marioSprites.SetSprite("jumpRight");
            }
            else
            {
                marioSprites.SetSprite("jumpLeft");
            }
        }
    }

    public void Jump()
    {
        if (isOnGround) // prevent double jump
        {
            Jumping = true;
            Falling = false;
            jumpStartHeight = location.Y;
            yVelocity = -JUMP_VELOCITY;
            isOnGround = false; // Mario is now in the air

            if(Direction)
            {
                marioSprites.SetSprite("jumpRight");
            }
            else
            {
                marioSprites.SetSprite("jumpLeft");
            }
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
        Falling = true;
        if (Direction)
        {
            marioSprites.SetAnimatedSprite("flagpoleRight");
        }
        else
        {
            marioSprites.SetAnimatedSprite("flagpoleLeft");
        }
    }
    public void EndFlagPole()
    {
        if (Direction)
            marioSprites.SetSprite("standRight");
        else
            marioSprites.SetSprite("standLeft");

        isOnGround = true;
    }
    public void Crouch()
    {
        if (Crouching)
        {
            if (Direction)
            {
                location = new Vector2(location.X, location.Y + 10f * (SCALE));
                marioSprites.SetLocation(location);
                marioSprites.SetSprite("crouchRight");
            }
            else if (!Direction)
            {
                location = new Vector2(location.X, location.Y + 10f * (SCALE));
                marioSprites.SetLocation(location);
                marioSprites.SetSprite("crouchLeft");
            }
        }
        else if (!Crouching)
        {
            if (Direction)
            {
                location = new Vector2(location.X, location.Y - 10f * (SCALE));
                marioSprites.SetLocation(location);
                marioSprites.SetSprite("standRight");
            }
            else if (!Direction)
            {
                location = new Vector2(location.X, location.Y - 10f * (SCALE));
                marioSprites.SetLocation(location);
                marioSprites.SetSprite("standLeft");
            }
        }
    }

    public void Fireball()
    {
        throwing = true;
        throwTimerMs = 0;
        if (Direction)
        {
            marioSprites.SetAnimatedSprite("throwRight");
        }
        else
        {
            marioSprites.SetAnimatedSprite("throwLeft");
        }
    }

    public void Damage()
    {

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

    public Boolean GetCollidable()
    {
        return true;
    }

    public void SetCollidable(Boolean state)
    {
        
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
                marioSprites.SetLocation(newlocation);

                // Check if reached peak
                if (newlocation.Y <= jumpStartHeight - 100)
                {
                    yVelocity = GRAVITY;
                    Falling = true;
                }
            }
            else
            {
                // Move down
                newlocation.Y += GRAVITY;
                marioSprites.SetLocation(newlocation);

                // Stop falling when reaching the ground
                if (newlocation.Y >= jumpStartHeight)
                {
                    newlocation.Y = jumpStartHeight;
                    Jumping = false;
                    Falling = false;
                    isOnGround = true;

                    if (Direction)
                        marioSprites.SetSprite("standRight");
                    else
                        marioSprites.SetSprite("standLeft");
                }
            }
        }

        location = newlocation;

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

        Collider = marioSprites.UpdateCollider();

        marioSprites.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        marioSprites.Draw(spriteBatch);
    }
}