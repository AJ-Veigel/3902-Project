using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using SprintZero.Marios;

public class SmallMario : IMario
{
    private MarioSprite marioSprites;
    public Vector2 location { get; set; }
    public Rectangle MarioCollider { get; set; }
    public float yVelocity { get; set; }
    public float xVelocity { get; set; }
    public float jumpStartHeight { get; set; }
    public Boolean Jumping { get; set; }
    public Boolean Falling { get; set; }
    public Boolean isOnGround { get; set; }
    public Boolean Direction { get; set; }
    public Boolean Sprinting { get; set; }
    public Boolean Crouching { get; set; }
    public Boolean Swimming { get; set; }
    public Boolean Moving { get; set; }
    private const float DefaultMoveSpeed = 4f;
    private const float SCALE = 4f;
    private const float GRAVITY = 0.2f;
    private float groundY;
    private float currentPlatformY;
    private const float JUMP_POWER = -8f;

    public SmallMario(TextureAtlas smallMarioTexture)
    {
        Moving = false;
        // default location
        location = new Vector2(300, 664);
        groundY = location.Y;
        currentPlatformY = groundY;

        // jump height based on location
        jumpStartHeight = location.Y;

        yVelocity = 0f;
        xVelocity = 0f;

        marioSprites = new MarioSprite(smallMarioTexture, 0, location);

        // Set Mario Collider
        MarioCollider = marioSprites.UpdateCollider();

        isOnGround = false;
    }
    public SmallMario(TextureAtlas smallMarioTexture, Vector2 pos)
    {
        Moving = false;
        // set location to the passed Vector2
        location = pos;

        groundY = location.Y;
        currentPlatformY = groundY;

        // jump height based on location
        jumpStartHeight = pos.Y;

        yVelocity = 0f;
        xVelocity = 0f;

        marioSprites = new MarioSprite(smallMarioTexture, 0, location);

        // Set Mario Collider
        MarioCollider = marioSprites.UpdateCollider();

        isOnGround = false;
    }

    public void Move()
    {
        Moving = true;
        if (Direction)
        {
            xVelocity = DefaultMoveSpeed;
        }
        else
        {
            xVelocity = -DefaultMoveSpeed;
        }
        if (Jumping & !isOnGround)
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
    public void StopMove()
    {
        Moving = false;
        xVelocity = 0;
        if (!Jumping && !Falling)
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

    }
    public void Damage()
    {
        marioSprites.SetSprite("death");
    }
    public void Fireball()
    {

    }
    public void GrabFlagPole()
    {
        Jumping = false;
        Falling = false;
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
    public void LandOnBlock(float blockTopY)
    {
        location = new Vector2(location.X, blockTopY - marioSprites.GetSprite().Height * SCALE);
        currentPlatformY = location.Y;
        yVelocity = 0f;
        Jumping = false;
        Falling = false;
        isOnGround = true;
        jumpStartHeight = location.Y;

        if (Direction)
            marioSprites.SetSprite("standRight");
        else
            marioSprites.SetSprite("standLeft");

        MarioCollider = marioSprites.UpdateCollider();
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
                Jumping = false;
        }

        if (Falling)
        {
            if (yVelocity <= -JUMP_POWER)
                yVelocity += GRAVITY;
            location = new Vector2(location.X, location.Y + yVelocity);
            marioSprites.SetLocation(location);

            if (isOnGround)
            {
                yVelocity = 0;
                location = new Vector2(location.X, currentPlatformY);
                Jumping = false;
                Falling = false;

                if (Direction)
                    marioSprites.SetSprite("standRight");
                else
                    marioSprites.SetSprite("standLeft");
            }
        }


        if (isOnGround)
        {
            if (!Moving) StopMove();
            Falling = false;
            yVelocity = 0f;
        } 
        else if (!isOnGround && !Jumping)
        {
            Falling = true;
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
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        marioSprites.Draw(spriteBatch);
    }
}