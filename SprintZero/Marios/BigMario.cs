using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Marios;

public class BigMario : IMario
{
    public Vector2 location { get; set; }
    private MarioSprite marioSprites;
    public Rectangle MarioCollider { get; set; }
    public float yVelocity { get; set; }
    public float xVelocity { get; set; }
    public float jumpStartHeight { get; set; }
    private float groundY;
    private float currentPlatformY;
    public Boolean Jumping { get; set; }
    public Boolean isOnGround { get; set; }
    public Boolean Falling { get; set; }
    // If direction is True, mario is facing right, if direction is false, mario is facing left
    public Boolean Direction { get; set; }
    public Boolean Sprinting { get; set; }
    public Boolean Crouching { get; set; }
    public Boolean Swimming { get; set; }
    private float DefaultMoveSpeed = 4f;
    private const float SCALE = 4f;
    private const float GRAVITY = 0.2f;
    private const float JUMP_POWER = -8f;

    public void Move()
    {
        if (!Crouching)
        {
            if (Direction)
            {
                xVelocity = DefaultMoveSpeed;
            }
            else
            {
                xVelocity = -DefaultMoveSpeed;
            }
            if (Jumping)
            {
                if (Direction)
                {
                    location = new Vector2(location.X + xVelocity, location.Y);
                    marioSprites.SetLocation(location);
                    marioSprites.SetSprite("jumpRight");
                }
                else if (!Direction)
                {
                    location = new Vector2(location.X + xVelocity, location.Y);
                    marioSprites.SetLocation(location);
                    marioSprites.SetSprite("jumpLeft");
                }
            }
            else if (!Jumping)
            {
                if (Direction)
                {
                    marioSprites.SetAnimatedSprite("moveRight");
                    location = new Vector2(location.X + xVelocity, location.Y);
                    marioSprites.SetLocation(location);
                }
                else if (!Direction)
                {
                    marioSprites.SetAnimatedSprite("moveLeft");
                    location = new Vector2(location.X + xVelocity, location.Y);
                    marioSprites.SetLocation(location);
                }
            }
        }
    }
    public void StopMove()
    {
        xVelocity = 0;
        if (!Jumping)
        {
            if (Direction)
            {
                marioSprites.SetSprite("standRight");
            }
            else if (!Direction)
            {
                marioSprites.SetSprite("standLeft");
            }
        }
    }
    public void LandOnBlock(float blockTopY)
    {
        location = new Vector2(location.X, blockTopY - marioSprites.GetSprite().Height * SCALE);
        isOnGround = true;
        Jumping = false;
        Falling = false;
        jumpStartHeight = location.Y;

        if (Direction)
            marioSprites.SetSprite("standRight");
        else
            marioSprites.SetSprite("standLeft");

        MarioCollider = marioSprites.UpdateCollider();
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
        }
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
    public void Damage()
    {

    }
    public void Fireball()
    {

    }
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
    public BigMario(TextureAtlas bigMarioTexture)
    {
        // Defaults
        location = new Vector2(300, 600);
        Direction = true;

        groundY = location.Y;
        currentPlatformY = groundY;

        yVelocity = 0f;
        xVelocity = 0f;

        marioSprites = new MarioSprite(bigMarioTexture, 1, location);

        // Set Mario Collider
        MarioCollider = marioSprites.UpdateCollider();

        isOnGround = true;
    }
    public BigMario(TextureAtlas bigMarioTexture, Vector2 pos)
    {
        // Defaults
        location = pos;
        Direction = true;

        groundY = location.Y;
        currentPlatformY = groundY;

        yVelocity = 0f;
        xVelocity = 0f;

        marioSprites = new MarioSprite(bigMarioTexture, 1, location);

        // Set Mario Collider
        MarioCollider = marioSprites.UpdateCollider();

        isOnGround = true;
    }
    public void Update(GameTime gameTime)
    {
        if (Jumping && !Falling)
        {
            yVelocity += GRAVITY;
            location = new Vector2(location.X, location.Y + yVelocity);
            marioSprites.SetLocation(location);

            if (yVelocity <= 0)
                Falling = true;
        }

        if (Falling)
        {
            if(yVelocity <= -JUMP_POWER)
                yVelocity += GRAVITY;
            location = new Vector2(location.X, location.Y + yVelocity);
            marioSprites.SetLocation(location);

            if (location.Y >= currentPlatformY)
            {
                yVelocity = 0;
                location = new Vector2(location.X, currentPlatformY);
                Jumping = false;
                Falling = false;
                isOnGround = true;

                if (Direction)
                    marioSprites.SetSprite("standRight");
                else
                    marioSprites.SetSprite("standLeft");
            }
        }


        if(isOnGround)
        {
            Falling = false;
            yVelocity = 0f;
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

        MarioCollider = marioSprites.UpdateCollider();


        marioSprites.Update(gameTime);
        MarioCollider = marioSprites.UpdateCollider();
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        marioSprites.Draw(spriteBatch);
    }
}